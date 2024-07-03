using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic {
    public class CreateGroupVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [RegularExpression("^[1-4]\\d\\d$", ErrorMessage = "Must be a 3 digit number between 100 and 499")]
        public string Number { get; set; } = null!;

        [Required(ErrorMessage = "Batch is required")]
        public string BatchId { get; set; }

        //public string StudyDomainId { get; set; }
    }
}
