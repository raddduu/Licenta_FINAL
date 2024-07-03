using ManageMe.Common.Interfaces;

namespace ManageMe.Entities
{
    public class ChannelRequest : IEntity
    {
        public string RequesterId { get; set; } 
        public int ChannelId { get; set; }
        public DateTime Date { get; set; }
        public virtual Channel Channel { get; set; } = null!;
        public virtual ApplicationUser Requester { get; set; } = null!;
    }
}
