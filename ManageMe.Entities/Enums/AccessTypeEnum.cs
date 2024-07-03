using System.ComponentModel.DataAnnotations;

namespace ManageMe.Entities.Enums
{
    public enum AccessTypeEnum
    {
        [Display(Name = "Free Access")]
        FreeAccess = 1,
        [Display(Name = "Join Code")]
        JoinCode = 2,
        [Display(Name = "Join Request")]
        JoinRequest = 3
    }
}
