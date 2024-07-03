using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ManageMe.BusinessLogic
{
    public class StudyDomainCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters", MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "StudyYears is required")]
        [Range(1, 4, ErrorMessage = "StudyYears must be between 1 and 4")]
        public int StudyYears { get; set; }
    }
}
