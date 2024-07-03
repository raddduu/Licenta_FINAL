using ManageMe.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ManageMe.Entities;

public partial class ApplicationRole : IdentityRole, IEntity
{
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    //public virtual ICollection<ChannelRole> ChannelRoles { get; set; } = new List<ChannelRole>();

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();
}
