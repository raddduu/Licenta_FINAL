using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class CreateStudyPlanVM
    {
        public int StudyDomainId { get; set; }

        public string SubjectId { get; set; } = null!;

        public string SubjectType { get; set; } = null!;

        public int LaboratoryCredits { get; set; }

        public int Semester { get; set; }

        public int CourseCredits { get; set; }

        public int SeminaryCredits { get; set; }

        public int ProjectCredits { get; set; }

        [Required(ErrorMessage = "Evaluation form is required")]
        public string EvaluationForm { get; set; } = null!;

        [Required(ErrorMessage = "Total credits is required")]
        public int TotalCredits { get; set; }

        public int SubjectOptionality { get; set; }
    }
}
