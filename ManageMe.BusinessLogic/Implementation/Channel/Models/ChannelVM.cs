using ManageMe.Entities;
using ManageMe.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using ManageMe.BusinessLogic;

namespace ManageMe.BusinessLogic
{
    public class ChannelVM : IIdentifiable
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Subject { get; set; } = null!;

        public string AccessType { get; set; } = null!;

        public IFormFile? ChannelPic { get; set; }
        public byte[]? ChannelPicByteArray { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public List<DetailsChannelUserVM> ChannelMembers { get; set; } = new List<DetailsChannelUserVM>();

        public List<ChannelRequesterUserVM> ChannelRequesters { get; set; } = new List<ChannelRequesterUserVM>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
