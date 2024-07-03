using ManageMe.DataAccess;
using ManageMe.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ManageMe.BusinessLogic
{
    public class GroupService : BaseService
    {
        private readonly GradingCriterionService _gradingCriterionService;
        public GroupService(ServiceDependencies serviceDependencies, GradingCriterionService gradingCriterionService) : base(serviceDependencies)
        {
            _gradingCriterionService = gradingCriterionService;
        }

        public bool GroupExists()
        {
            return UnitOfWork.Groups.Get().Any();
        }

        public int? GetgroupIdForStudentId(string userId)
        {
            return UnitOfWork.Groups.Get().FirstOrDefault(g => g.Users.Any(u => u.Id == userId))?.Id;
        }

        public Entities.Group? GetGroupById(int? id)
        {
            return UnitOfWork.Groups?.Get().FirstOrDefault(e => e.Id == id);
        }

        public DetailsGroupVM GetCompleteGroup(int? id)
        {
            var currentSemester = UnitOfWork.Semesters.Get().FirstOrDefault()?.SemesterNumber ?? 1;

            var currentStudyYear = UnitOfWork.Groups.Get()
                .Where(g => g.Id == id)
                .Include(g => g.Batch)
                .Select(g => g.Batch.Year)
                .FirstOrDefault();

            var mappedGroup = UnitOfWork.Groups.Get()
                            .Where(g => g.Id == id)
                            .Include(g => g.Batch)
                                .ThenInclude(b => b.StudyDomain)
                                    .ThenInclude(sd => sd.StudyPlans)
                                        .ThenInclude(sp => sp.Subject)
                            .Include(g => g.TeacherPermissions)
                                .ThenInclude(tp => tp.Teacher)
                            .Include(g => g.Users)
                            .Select(g => new DetailsGroupVM
                            {
                                Id = g.Id,
                                Number = g.Number,
                                StudyYear = g.Batch.Year,
                                Students = g.Users.OrderBy(u => u.LastName).Select(u => Mapper.Map<UserMinimalInfo>(u)).ToList(),
                                Subjects = g.Batch.StudyDomain.StudyPlans
                                .Where(sp => (sp.Semester - currentSemester) % 2 == 0
                                    && (sp.Semester + 1) / 2 == currentStudyYear
                                    && sp.SubjectOptionality == 1)
                                .Select(sp => new SubjectInfoForGroup
                                {
                                    Id = sp.Subject.Id,
                                    Name = sp.Subject.Name,
                                    CourseTeacherNames = g.TeacherPermissions.Where(tp => tp.ActivityId == 4 && tp.SubjectId == sp.SubjectId).Select(tp => Tuple.Create(tp.Teacher.FirstName + " " + tp.Teacher.LastName, tp.TeacherId)).ToList(),
                                    LaboratoryTeacherNames = g.TeacherPermissions.Where(tp => tp.ActivityId == 3 && tp.SubjectId == sp.SubjectId).Select(tp => Tuple.Create(tp.Teacher.FirstName + " " + tp.Teacher.LastName, tp.TeacherId)).ToList(),
                                    SeminaryTeacherNames = g.TeacherPermissions.Where(tp => tp.ActivityId == 6 && tp.SubjectId == sp.SubjectId).Select(tp => Tuple.Create(tp.Teacher.FirstName + " " + tp.Teacher.LastName, tp.TeacherId)).ToList(),
                                    HasCourse = sp.CourseCredits != 0,
                                    HasLaboratory = sp.LaboratoryCredits != 0,
                                    HasSeminary = sp.SeminaryCredits != 0
                                }).ToList()
                            })
                            .SingleOrDefault();

            mappedGroup?.Subjects.ForEach(s => s.SubjectGradingCriterion =
                _gradingCriterionService?.GetDetailsGradingCriterionVM(s.Id, mappedGroup.Id) ?? new DetailsGradingCriterionVM());

            return mappedGroup ?? new DetailsGroupVM { };
        }

        public List<IndexGroupVM> GetAllGroups(int? batchId)
        {
            var groups = UnitOfWork.Groups.Get()
                        .Where(g => batchId == null || g.BatchId == batchId)
                        .Include(g => g.Batch)
                        .ThenInclude(b => b.StudyDomain)
                        .OrderBy(g => g.Number)
                        .Select(g => Mapper.Map<IndexGroupVM>(g));

            return groups.ToList();
        }

        public void CreateGroup(CreateGroupVM createGroupVM)
        {
            var group = Mapper.Map<Entities.Group>(createGroupVM);
            UnitOfWork.Groups.Insert(group);
            UnitOfWork.SaveChanges();
        }

        public void DeleteGroup(int id)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                UnitOfWork.Groups.Delete(group);
                UnitOfWork.SaveChanges();
            }
        }

        public void AddStudentToGroup(int groupId, string userId)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == groupId);
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(u => u.Id == userId);
            if (group != null && user != null)
            {
                group.Users.Add(user);
                UnitOfWork.Groups.Update(group);
                UnitOfWork.SaveChanges();
            }
        }

        public void RemoveStudentFromGroup(int groupId, string studentId)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == groupId);
            var student = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(u => u.Id == studentId);
            if (group != null && student != null)
            {
                group.Users.Remove(student);
                UnitOfWork.Groups.Update(group);
                UnitOfWork.SaveChanges();
            }
        }

        public void AddTeacherToGroup(int groupId, string teacherId, int subjectId, int activityId)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == groupId);
            var teacherPermission = UnitOfWork.TeacherPermissions.Get()
                                    .FirstOrDefault(tp => tp.TeacherId == teacherId && tp.SubjectId == subjectId && tp.ActivityId == activityId);

            if (group != null && teacherPermission != null)
            {
                group.TeacherPermissions.Add(teacherPermission);
                teacherPermission.Groups.Add(group);
                UnitOfWork.Groups.Update(group);
                UnitOfWork.TeacherPermissions.Update(teacherPermission);
                UnitOfWork.SaveChanges();
            }
        }

        public void RemoveTeacherFromGroup(int groupId, string teacherId, int subjectId, int activityId)
        {
            var group = UnitOfWork.Groups.Get().Include(g => g.TeacherPermissions).FirstOrDefault(g => g.Id == groupId);
            var teacherPermission = UnitOfWork.TeacherPermissions.Get()
                                    .FirstOrDefault(tp => tp.TeacherId == teacherId && tp.SubjectId == subjectId && tp.ActivityId == activityId);

            if (group != null && teacherPermission != null)
            {
                group.TeacherPermissions.Remove(teacherPermission);
                teacherPermission.Groups.Remove(group);
                UnitOfWork.Groups.Update(group);
                UnitOfWork.TeacherPermissions.Update(teacherPermission);
                UnitOfWork.SaveChanges();
            }
        }

        public int? GetGroupByUserId(string userId)
        {
            var groupId = UnitOfWork.ApplicationUsers.Get()
                            .FirstOrDefault(u => u.Id == userId)
                            ?.GroupId;

            return groupId;
        }

        public IEnumerable<TeacherGroupVM>? GetGroupsByTeacherId(string? teacherId)
        {
            var groups = UnitOfWork.Groups.Get()
                        .Include(g => g.TeacherPermissions.Where(tp => tp.TeacherId == teacherId))
                                .ThenInclude(tp => tp.Teacher)
                        .Include(g => g.Batch)
                        .ThenInclude(b => b.StudyDomain)
                                    .ThenInclude(sd => sd.StudyPlans)
                                        .ThenInclude(sp => sp.Subject)
                        .Include(g => g.TeacherPermissions)
                                .ThenInclude(tp => tp.Activity)
                        .Where(g => g.TeacherPermissions.Any(tp => tp.TeacherId == teacherId))
                        .OrderBy(g => g.Number)
                        .Select(g => Mapper.Map<TeacherGroupVM>(g));
            

            return groups.ToList();
        }

        public bool? UserIsInGroup(string currentUserId, int channelId)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == channelId);
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(u => u.Id == currentUserId);
            if (group != null && user != null)
            {
                return group.Users.Contains(user);
            }
            return null;
        }

        public bool CurrentUserHasThePermissionToCoordinateThisActivity(string? currentUserId, int groupId, int subjectId, int activityId)
        {
            var group = UnitOfWork.Groups.Get().Include(g => g.TeacherPermissions).FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                return false;
            }

            var hasTeacherPermissionForThisGroupAndActivity =
                group.TeacherPermissions.Any(tp => tp.TeacherId == currentUserId
                                                   && tp.SubjectId == subjectId
                                                   && tp.ActivityId == activityId);
            
            return hasTeacherPermissionForThisGroupAndActivity;
        }

        public Classbook? GetClassbook(int groupId, int subjectId, int gradingActivityId)
        {
            var activityId = UnitOfWork.GradingActivities.Get().Where(ga => ga.Id == gradingActivityId).Select(a => a.ActivityId).FirstOrDefault();

            var studyDomainId = UnitOfWork.Groups.Get()
                .Where(g => g.Id == groupId)
                .Include(g => g.Batch)
                .Select(g => g.Batch.StudyDomainId).FirstOrDefault();

            var classBook = UnitOfWork.Groups.Get()
                                .Where(g => g.Id == groupId)
                                .Include(g => g.Users)
                                    .ThenInclude(u => u.Grades)
                                .Include(g => g.TeacherPermissions)
                                    .ThenInclude(tp => tp.Activity)
                                        .ThenInclude(a => a.GradingActivities)
                                .Select(g => new Classbook
                                {
                                    Id = g.Id,
                                    SubjectName = UnitOfWork.Subjects.Get().Where(s => s.Id == subjectId).Select(s => s.Name).FirstOrDefault(),
                                    GradingActivityName = UnitOfWork.GradingActivities.Get().Where(a => a.Id == gradingActivityId).Select(a => a.Name).FirstOrDefault(),
                                    Students = g.Users.OrderBy(u => u.LastName).Select(u => Mapper.Map<StudentGradesForSubjectVM>(u)).ToList(),
                                    SubjectId = subjectId,
                                    GradingActivityId = gradingActivityId,
                                    //SubjectActivityFrequencyValue = UnitOfWork.SubjectActivityFrequencies.Get()
                                    //                                .Where(saf => saf.SubjectId == subjectId
                                    //                                              && saf.ActivityId == activityId
                                    //                                              && saf.StudyDomainId == studyDomainId)
                                    //                                .Select(saf => saf.SubjectActivityFrequencyId)
                                    //                                .FirstOrDefault()
                                })
                                .SingleOrDefault();

            return classBook;
        }

        public List<GradingActivity> GetGroupGradingActivitiesByActivityId(int groupId, int subjectId, int activityId)
        {
            var gradingCriteriaForThisGroupAtThisSubject = UnitOfWork.GradingCriteria.Get()
                                                            .Where(gc => gc.GroupId == groupId && gc.SubjectId == subjectId);

            if (gradingCriteriaForThisGroupAtThisSubject.Count() == 0)
            {
                return new List<GradingActivity>();
            }

            var gradingActivitiesForThisGroupAtThisSubject = UnitOfWork.GradingActivities.Get()
                                                            .Where(ga => ga.ActivityId == activityId
                                                                         && gradingCriteriaForThisGroupAtThisSubject.Any(gc => gc.GradingActivityId == ga.Id))
                                                            .ToList();

            return gradingActivitiesForThisGroupAtThisSubject;
        }

        public bool CurrentUserHasThePermissionToCoordinateThisGradingActivity(string currentUserId, int groupId, int subjectId, int gradingActivityId)
        {
            var activityId = UnitOfWork.GradingActivities.Get().Where(ga => ga.Id == gradingActivityId).Select(a => a.ActivityId).FirstOrDefault();
            
            return CurrentUserHasThePermissionToCoordinateThisActivity(currentUserId, groupId, subjectId, activityId);
        }

        public List<GradingActivity> GetGroupGradingActivitiesByGradingActivityId(int groupId, int subjectId, int gradingActivityId)
        {
            var activityId = UnitOfWork.GradingActivities.Get().Where(ga => ga.Id == gradingActivityId).Select(a => a.ActivityId).FirstOrDefault();
            
            return GetGroupGradingActivitiesByActivityId(groupId, subjectId, activityId);
        }

        public bool AllStudentsAreInTheSameGroup(List<string> studentIds)
        {
            var studentGroups = UnitOfWork.ApplicationUsers.Get()
                                    .Where(u => studentIds.Contains(u.Id))
                                    .Select(u => u.GroupId)
                                    .Distinct()
                                    .ToList();

            return studentGroups.Count == 1;
        }

        public List<Tuple<string, int>> GetAllGroupNumbers()
        {
            var result = UnitOfWork.Groups.Get()
                            .Select(g => new Tuple<string, int>(g.Number, g.Id))
                            .ToList();

            return result;
        }

        public int? GetStudentGroupId(string userId)
        {
            var groupId = UnitOfWork.ApplicationUsers.Get()
                            .Where(u => u.Id == userId)
                            .Select(u => u.GroupId)
                            .FirstOrDefault();

            return groupId;
            
        }

        public List<MinimalGroupInfo> GetGroupsWhereUserIsLectorAtThisSubject(string userId, int subjectId)
        {
            return UnitOfWork.Groups.Get()
                    .Include(g => g.TeacherPermissions)
                        .ThenInclude(tp => tp.Teacher)
                    .Where(g => g.TeacherPermissions.Any(tp => tp.TeacherId == userId && tp.SubjectId == subjectId && tp.ActivityId == 4))
                    .Select(g => new MinimalGroupInfo
                    {
                        Id = g.Id,
                        Number = g.Number
                    })
                    .ToList();
        }
    }
}
