using ManageMe.Entities;
using ManageMe.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using ManageMe.BusinessLogic;
using ManageMe.BusinessLogic.Implementation;

namespace ManageMe.BusinessLogic
{
    public class DetailsChannelVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Subject { get; set; } = null!;

        public string AccessType { get; set; } = null!;
        public string? JoinCode { get; set; }

        public IFormFile? ChannelPic { get; set; }
        public byte[]? ChannelPicByteArray { get; set; }

        public List<DetailsChannelUserVM> ChannelMembers { get; set; } = new List<DetailsChannelUserVM>();
        public List<ApplicationRole> AllowedRoles { get; set; } = new List<ApplicationRole>();
        public List<ChannelRequesterUserVM> ChannelRequesters { get; set; } = new List<ChannelRequesterUserVM>();
    }
}
