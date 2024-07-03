namespace ManageMe.BusinessLogic
{
    public class DetailsStudyPlanVM
    {
        public int StudyDomainId { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = null!;

        public string SubjectTypeName { get; set; } = null!;

        public int StudyYear { get; set; }

        public int StudySemester { get; set;}

        public int CourseCredits { get; set; }

        public int SeminaryCredits { get; set; }

        public int ProjectCredits { get; set; }

        public int LaboratoryCredits { get; set; }

        public string EvaluationFormName { get; set; } = null!;

        public int SubjectOptionality { get; set; }

        public int TotalCredits { get; set; }

        public string? CourseFrequency { get; set; }

        public string? SeminaryFrequency { get; set; }

        public string? LaboratoryFrequency { get; set; }
    }
}
