using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class BatchCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public string Number { get; set; } = null!;

        public bool IsForOlympiadParticipants { get; set; }

        [Required(ErrorMessage = "Study domain is required")]
        public int StudyDomainId { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1, 4, ErrorMessage = "Study Year must be between 1 and 4")]
        public int Year { get; set; }
    }
}
