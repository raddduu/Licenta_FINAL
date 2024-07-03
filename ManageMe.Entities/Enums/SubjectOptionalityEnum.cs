using System.ComponentModel.DataAnnotations;

namespace ManageMe.Entities.Enums
{
    public enum SubjectOptionalityEnum
    {
        [Display(Name = "Mandatory")]
        Mandatory = 1,

        [Display(Name = "Optional")]
        Optional = 2,

        [Display(Name = "Facultative")]
        Facultative = 3
    }
}
