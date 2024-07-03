using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class Message : IEntity
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int MessageTypeId { get; set; }

    public int? ParentMessageId { get; set; }

    public bool? IsAnnouncement { get; set; }

    public string AuthorId { get; set; }

    public DateTime? Time { get; set; }

    public int? ChannelId { get; set; }

    public virtual Channel? Channel { get; set; }

    public virtual ApplicationUser? Author { get; set; } = null!;

    public virtual ICollection<Message> ChildrenMessages { get; set; } = new List<Message>();

    public virtual Message? ParentMessage { get; set; }
}
