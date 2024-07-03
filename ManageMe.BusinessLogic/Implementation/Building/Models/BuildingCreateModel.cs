using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class BuildingCreateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;
    }
}
