using ManageMe.Entities;
using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class SubjectCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Short name is required")]
        [StringLength(10, ErrorMessage = "Short name must be between 2 and 10 characters", MinimumLength = 2)]
        public string ShortName { get; set; }
    }
}
