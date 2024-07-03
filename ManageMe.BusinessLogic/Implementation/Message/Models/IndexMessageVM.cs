using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class IndexMessageVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int MessageTypeId { get; set; }

        public bool? IsAnnouncement { get; set; }

        public DateTime? Time { get; set; }

        public virtual Channel? Channel { get; set; }

        public virtual ApplicationUser? Author { get; set; } = null!;

        public virtual ICollection<Message> ChildrenMessages { get; set; } = new List<Message>();

        public virtual Message? ParentMessage { get; set; }
    }
}
