using ManageMe.Common.Extensions;
using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.BusinessLogic
{
    public class GradingCriterionService : BaseService
    {
        public GradingCriterionService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

        }

        public bool GradingCriterionExists(int subjectId, int groupId)
        {
            return UnitOfWork.GradingCriteria.Get().Any(gc => gc.SubjectId == subjectId && gc.GroupId == groupId);
        }

        public bool CreateGradingCriterion(CreateGradingCriterionModel gradingCriterion)
        {
            try
            {
                var dbGradingCriterionSubject = new GradingCriterionSubject
                {
                    SubjectId = gradingCriterion.SubjectId,
                    GroupId = gradingCriterion.GroupId,
                    MinimumPointsRequired = gradingCriterion.MinimumPointsForPassSubject,
                    BonusPoints = gradingCriterion.BonusPoints,
                    BonusPointsMattersForPassingSubject = gradingCriterion.BonusMattersForPassingTheSubject
                };

                try
                {
                    UnitOfWork.GradingCriterionSubjects.Insert(dbGradingCriterionSubject);
                    UnitOfWork.SaveChanges();
                }
                catch
                {
                    UnitOfWork.GradingCriterionSubjects.Update(dbGradingCriterionSubject);
                    UnitOfWork.SaveChanges();
                }

                var properties = gradingCriterion.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (!property.Name.Contains("Bonus")
                        && !property.Name.Contains("MinimumPointsForPass")
                        && !property.Name.Contains("Id"))
                    {
                        if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                        {
                            if (property.GetValue(gradingCriterion) != null && (decimal?)property.GetValue(gradingCriterion) != 0)
                            {
                                var gradingActivityName = property.Name.Replace("Points", "").GetSpaceNameFromCamelCase().Trim();

                                var correspondingMinimumPointsProperty
                                    = properties.FirstOrDefault(e => e.Name == "MinimumPointsForPass" + gradingActivityName.Replace(" ", ""));

                                var minimumCorrespondingpropertyValue = (decimal?)correspondingMinimumPointsProperty?.GetValue(gradingCriterion);

                                var correspondingMattersForPassingTheSubjectProperty
                                    = properties.FirstOrDefault(e => e.Name == gradingActivityName.Replace(" ", "") + "MattersForPassingTheSubject");

                                var mattersForPassingTheSubject = (bool?)correspondingMattersForPassingTheSubjectProperty?.GetValue(gradingCriterion);

                                var gradingActivityId = UnitOfWork.GradingActivities.Get()
                                    .FirstOrDefault(e => e.Name == gradingActivityName)?.Id ?? 0;

                                var dbGradingCriterion = new GradingCriterion
                                {
                                    SubjectId = gradingCriterion.SubjectId,
                                    GroupId = gradingCriterion.GroupId,
                                    GradingActivityId = gradingActivityId,
                                    Points = (decimal?)property.GetValue(gradingCriterion),
                                    MinimumPointsRequired = minimumCorrespondingpropertyValue,
                                    MattersForPassingTheSubject = mattersForPassingTheSubject ?? false
                                };
                                
                                if (UnitOfWork.GradingCriteria.Get().Any(gc => gc.SubjectId == gradingCriterion.SubjectId
                                                                   && gc.GroupId == gradingCriterion.GroupId
                                                                                                      && gc.GradingActivityId == gradingActivityId))
                                {
                                    UnitOfWork.GradingCriteria.Update(dbGradingCriterion);
                                }
                                else
                                {
                                    UnitOfWork.GradingCriteria.Insert(dbGradingCriterion);
                                }
                            }
                        }
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

        public DetailsGradingCriterionVM? GetDetailsGradingCriterionVM(int subjectId, int groupId)
        {
            var pointsAllocation = new List<ActivityGrade>();
            var minimumPointsFromActivitiesToPass = new List<ActivityGrade>();
            var activitiesSummatedToComputePassEvaluation = new List<string>();

            var gradingCriteria = UnitOfWork.GradingCriteria.Get()
                .Include(e => e.GradingActivity)
                .Where(e => e.SubjectId == subjectId && e.GroupId == groupId);
                //.ToList();

            var gradingCriterionSubject = UnitOfWork.GradingCriterionSubjects.Get()
                .FirstOrDefault(e => e.SubjectId == subjectId && e.GroupId == groupId);

            if (gradingCriterionSubject == null)
            {
                return null;
            }

            var bonusPoints = gradingCriterionSubject.BonusPoints;
            var bonusPointsMattersForPassingTheSubject = gradingCriterionSubject.BonusPointsMattersForPassingSubject;

            var gradingActivities = UnitOfWork.GradingActivities.Get().ToList();

            foreach (var gradingActivity in gradingActivities)
            {
                var gradingCriterion = gradingCriteria.FirstOrDefault(e => e.GradingActivityId == gradingActivity.Id);

                if (gradingCriterion != null)
                {
                    pointsAllocation.Add(new ActivityGrade
                    {
                        ActivityName = gradingActivity.Name,
                        Points = gradingCriterion.Points,
                    });

                    if (gradingCriterion.MattersForPassingTheSubject)
                    {
                        activitiesSummatedToComputePassEvaluation.Add(gradingActivity.Name);
                    }

                    if (gradingCriterion.MinimumPointsRequired != null)
                    {
                        minimumPointsFromActivitiesToPass.Add(new ActivityGrade
                        {
                            ActivityName = gradingActivity.Name,
                            Points = gradingCriterion.MinimumPointsRequired,
                        });
                    }
                }
            }

            var gradingCriterionSubjectVM = new DetailsGradingCriterionVM
            {
                SubjectId = subjectId,
                MinimalPointsToPass = gradingCriterionSubject.MinimumPointsRequired,
                PointsAllocation = pointsAllocation,
                MinimumPointsFromActivitiesToPass = minimumPointsFromActivitiesToPass,
                ActivitiesSummatedToComputePassEvaluation = activitiesSummatedToComputePassEvaluation
            };

            if (bonusPoints != null)
            {
                gradingCriterionSubjectVM.PointsAllocation.Add(new ActivityGrade
                {
                    ActivityName = "Bonus",
                    Points = bonusPoints,
                });

                if (bonusPointsMattersForPassingTheSubject)
                {
                    gradingCriterionSubjectVM.ActivitiesSummatedToComputePassEvaluation.Add("Bonus");
                }
            }

            return gradingCriterionSubjectVM;
        }

        public bool AddGradingCriterion(GradingCriterion gradingCriterion)
        {
            try
            {
                UnitOfWork.GradingCriteria.Insert(gradingCriterion);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CurrentUserIsLectorForThisGroupForThisSubject(string currentUserId, int groupId, int subjectId)
        {
            var group = UnitOfWork.Groups.Get().Include(g => g.TeacherPermissions)
                            .SingleOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                return false;
            }

            return group.TeacherPermissions.Any(tp => tp.SubjectId == subjectId
                                                      && tp.TeacherId == currentUserId
                                                      && tp.ActivityId == 4);
        }

        public CreateGradingCriterionModel? GetGradingCriterionBySubjectIdAndGroupId(int subjectId, int groupId)
        {
            var dbGradingCriterion = UnitOfWork.GradingCriteria.Get()
                .FirstOrDefault(e => e.SubjectId == subjectId && e.GroupId == groupId);

            if (dbGradingCriterion == null)
            {
                return null;
            }

            var dbGradingCriterionSubject = UnitOfWork.GradingCriterionSubjects.Get().Where(e => e.SubjectId == subjectId && e.GroupId == groupId).FirstOrDefault();

            if (dbGradingCriterionSubject == null)
            {
                return null;
            }

            var viewGradingCriterion = new CreateGradingCriterionModel
            {
                SubjectId = subjectId,
                GroupId = groupId,
                MinimumPointsForPassSubject = dbGradingCriterionSubject.MinimumPointsRequired,
                BonusPoints = dbGradingCriterionSubject.BonusPoints,
                BonusMattersForPassingTheSubject = dbGradingCriterionSubject.BonusPointsMattersForPassingSubject
            };

            var gradingActivities = UnitOfWork.GradingActivities.Get().ToList();

            foreach (var gradingActivity in gradingActivities)
            {
                var gradingCriterion = UnitOfWork.GradingCriteria.Get()
                    .FirstOrDefault(e => e.GradingActivityId == gradingActivity.Id && e.SubjectId == subjectId && e.GroupId == groupId);

                if (gradingCriterion != null)
                {
                    var gradingActivityName = gradingActivity.Name.Replace(" ", "");

                    var pointsProperty = viewGradingCriterion.GetType().GetProperty(gradingActivityName + "Points");
                    pointsProperty?.SetValue(viewGradingCriterion, gradingCriterion.Points);

                    var minimumPointsProperty = viewGradingCriterion.GetType().GetProperty("MinimumPointsForPass" + gradingActivityName);
                    minimumPointsProperty?.SetValue(viewGradingCriterion, gradingCriterion.MinimumPointsRequired);

                    var mattersForPassingTheSubjectProperty = viewGradingCriterion.GetType().GetProperty(gradingActivityName + "MattersForPassingTheSubject");
                    mattersForPassingTheSubjectProperty?.SetValue(viewGradingCriterion, gradingCriterion.MattersForPassingTheSubject);
                }
            }

            return viewGradingCriterion;
        }

        public bool UpdateGradingCriterion(GradingCriterion gradingCriterion)
        {
            try
            {
                UnitOfWork.GradingCriteria.Update(gradingCriterion);
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
