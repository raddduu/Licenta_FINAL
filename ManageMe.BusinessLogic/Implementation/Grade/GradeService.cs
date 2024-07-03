using ManageMe.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.BusinessLogic
{
    public class GradeService : BaseService
    {
        public GradeService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<ClassbookStudentVM> GetFinalGrades(int groupId, int subjectId)
        {
            var minimumCummulativePoints = UnitOfWork.GradingCriterionSubjects.Get()
                .Where(gcs => gcs.GroupId == groupId && gcs.SubjectId == subjectId)
                .Select(gcs => gcs.MinimumPointsRequired)
                .FirstOrDefault();

            var idsForGradingActivitiesThatMatterForPassingTheSubject = UnitOfWork.GradingCriteria.Get()
                .Where(gc => gc.GroupId == groupId && gc.SubjectId == subjectId && gc.MattersForPassingTheSubject)
                .Select(gc => gc.GradingActivityId)
                .ToList();

            var groupStudentsDataPerStudent = UnitOfWork.StudentGradeSubjects.Get()
                .Where(sgs => sgs.GroupId == groupId && sgs.SubjectId == subjectId)
                .ToList()
                .GroupBy(sgs => new { sgs.StudentId, sgs.FirstName, sgs.LastName, sgs.Email, sgs.TotalPoints, sgs.TotalPointsRounded, sgs.GroupId });

            var studentgrades = new List<ClassbookStudentVM>();
            
            foreach (var studentData in groupStudentsDataPerStudent)
            {
                var passedMandatory = !studentData.Any(s => s.GradingActivityPassed == false);

                decimal sumOfGradesThatMatterForPassingTheSubject = 0;

                foreach (var id in idsForGradingActivitiesThatMatterForPassingTheSubject)
                {
                    var grade = studentData.FirstOrDefault(s => s.GradingActivityId == id);

                    if (grade != null)
                    {
                        sumOfGradesThatMatterForPassingTheSubject += grade.GradingActivityTotalPoints;
                    }
                }

                if (sumOfGradesThatMatterForPassingTheSubject < minimumCummulativePoints)
                {
                    passedMandatory = false;
                }

                var studentgrade = new ClassbookStudentVM
                {
                    Id = studentData.Key.StudentId,
                    SubjectId = subjectId,
                    Name = studentData.Key.FirstName + " " + studentData.Key.LastName,
                    Email = studentData.Key.Email,
                    TotalPoints = studentData.Key.TotalPoints,
                    RoundedGrade = studentData.Key.TotalPointsRounded,
                    GroupId = studentData.Key.GroupId,
                    PassStatus = passedMandatory ? "Passed" : "Failed",
                    ActivityGrades = studentData.Where(s => s.GradingActivityPassed == false)
                        .Select(s => new ActivityGrade
                        {
                            ActivityName = UnitOfWork.GradingActivities.Get()
                                ?.Single(ga => ga.Id == s.GradingActivityId)
                                .Name ?? "",
                            Points = s.GradingActivityTotalPoints,
                        })
                        .ToList()
                };

                studentgrades.Add(studentgrade);
            }
            //    .Select(sgs => new ClassbookStudentVM
            //    {
            //        Id = sgs.Key.StudentId,
            //        SubjectId = subjectId,
            //        Name = sgs.Key.FirstName + " " + sgs.Key.LastName,
            //        Email = sgs.Key.Email,
            //        TotalPoints = sgs.Key.TotalPoints,
            //        RoundedGrade = sgs.Key.TotalPointsRounded,
            //        PassStatus = sgs.Any(s => s.GradingActivityPassed == false || s.Status != "Passed") ? "Failed" : "Passed",
            //        GroupId = sgs.Key.GroupId,
            //        ActivityGrades = sgs.Where(s => s.GradingActivityPassed == false || s.Status != "Passed")
            //            .Select(s => new ActivityGrade
            //            {
            //                ActivityName = UnitOfWork.GradingActivities.Get()
            //                                ?.Single(ga => ga.Id == s.GradingActivityId)
            //                                .Name ?? "",
            //                Points = s.GradingActivityTotalPoints,
            //            })
            //            .ToList()
            //    })
            //    .ToList();

            return studentgrades;
        }

        public bool UpdateGrades(List<GradeCreateModel> gradeCreateModels, int subjectId, int gradingActivityId)
        {
            try
            {
                var grades = UnitOfWork.Grades.Get().Where(g => g.SubjectId == subjectId
                && g.GradingActivityId == gradingActivityId
                && gradeCreateModels.Select(gcm => gcm.StudentId).Contains(g.StudentId)
                && gradeCreateModels.Select(gcm => gcm.WeekNumber).Contains(g.WeekNumber)).ToList();

                foreach (var gradeCreateModel in gradeCreateModels)
                {
                    var grade = grades.FirstOrDefault(g => g.StudentId == gradeCreateModel.StudentId
                                                                          && g.WeekNumber == gradeCreateModel.WeekNumber);

                    if (grade == null)
                    {
                        grade = new Grade
                        {
                            StudentId = gradeCreateModel.StudentId,
                            SubjectId = subjectId,
                            GradingActivityId = gradingActivityId,
                            WeekNumber = gradeCreateModel.WeekNumber,
                            Value = gradeCreateModel.Value
                        };

                        UnitOfWork.Grades.Insert(grade);
                    }
                    else
                    {
                        grade.Value = gradeCreateModel.Value;

                        UnitOfWork.Grades.Update(grade);
                    }
                }

                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
