using System.ComponentModel.DataAnnotations;

namespace ManageMe.Entities.Enums
{
    public enum ActivityFrequencyEnum
    {
        [Display(Name = "Weekly")]
        IsHeldWeekly = 1,

        [Display(Name = "Even Weeks")]
        IsHeldEveryEvenWeek = 2,

        [Display(Name = "Odd Weeks")]
        IsHeldEveryOddWeek = 3
    }
}
