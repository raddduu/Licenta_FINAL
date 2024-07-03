namespace ManageMe.BusinessLogic
{
    public class DetailsGradingCriterionVM
    {
        public int SubjectId { get; set; }
        public List<ActivityGrade> PointsAllocation { get; set; } = new List<ActivityGrade>();

        public List<ActivityGrade> MinimumPointsFromActivitiesToPass { get; set; } = new List<ActivityGrade>();

        public List<string> ActivitiesSummatedToComputePassEvaluation { get; set; } = new List<string>();

        public decimal? MinimalPointsToPass { get; set; }
    }
}
