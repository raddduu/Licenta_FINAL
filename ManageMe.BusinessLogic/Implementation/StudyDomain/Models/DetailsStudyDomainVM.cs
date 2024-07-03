namespace ManageMe.BusinessLogic
{
    public class DetailsStudyDomainVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; }

        public int StudyYears { get; set; }

        public List<DetailsStudyPlanVM> StudyPlans { get; set; } = new List<DetailsStudyPlanVM>();
    }
}
