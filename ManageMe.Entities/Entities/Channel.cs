using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class Channel : IEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int SubjectId { get; set; }
    public int AccessTypeId { get; set; }

    public byte[]? ChannelPic { get; set; }

    public string? JoinCode { get; set; }

    public virtual Subject? Subject { get; set; } = null!;

    public virtual ICollection<ChannelUser> ChannelUsers { get; set; } = new List<ChannelUser>();
    public virtual ICollection<ChannelRequest> ChannelRequests { get; set; } = new List<ChannelRequest>();

    public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; } = new List<ApplicationRole>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

}
