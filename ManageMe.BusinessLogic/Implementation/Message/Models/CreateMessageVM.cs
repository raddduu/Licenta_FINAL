using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class CreateMessageVM
    {
        //[Required(ErrorMessage = "Text is required")]
        [StringLength(1000, ErrorMessage = "Text must be between 1 and 1000 characters", MinimumLength = 1)]
        public string Text { get; set; } = null!;

        public int MessageTypeId { get; set; }

        public int? ParentMessageId { get; set; }

        public bool? IsAnnouncement { get; set; }

        public int ChannelId { get; set; }
    }
}
