namespace ManageMe.BusinessLogic
{
    public class UpdateStudyPlanVM
    {
        public int StudyDomainId { get; set; }

        public int SubjectId { get; set; }

        public int SubjectType { get; set; }

        public int Semester { get; set; }

        public int CourseCredits { get; set; }

        public int SeminaryCredits { get; set; }

        public int ProjectCredits { get; set; }

        public int LaboratoryCredits { get; set; }

        public int EvaluationForm { get; set; }

        public int SubjectOptionality { get; set; }

        public int TotalCredits { get; set; }
    }
}
