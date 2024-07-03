using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class HallCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public int Number { get; set; }

        [MaxLength(1, ErrorMessage = "Additional letter for identification must be a single character")]
        public string? AdditionalLetter { get; set; }

        [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 300, ErrorMessage = "Capacity must be between 1 and 300")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Please specify if the hall has or does not have computers")]
        public bool HasComputers { get; set; }

        [Required(ErrorMessage = "Floor is required")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Building is required")]
        public int BuildingId { get; set; }
    }
}
