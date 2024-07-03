using System.ComponentModel.DataAnnotations;

namespace ManageMe.Entities.Enums
{
    public enum SubjectActivityFrequencyEnum
    {
        [Display(Name = "Weekly")]
        Weekly = 1,

        [Display(Name = "Even Weeks")]
        EvenWeeks = 2,

        [Display(Name = "Odd Weeks")]
        OddWeeks = 3
    }
}
