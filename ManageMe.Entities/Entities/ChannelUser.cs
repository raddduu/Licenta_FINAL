using ManageMe.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageMe.Entities
{
    public class ChannelUser : IEntity
    {
        public string UserId { get; set; }
        public int ChannelId { get; set; }
        public bool IsModerator { get; set; }
        public virtual Channel? Channel { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
