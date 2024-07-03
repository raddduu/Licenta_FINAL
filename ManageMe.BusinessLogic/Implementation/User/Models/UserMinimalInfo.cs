using ManageMe.Entities;
using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class UserMinimalInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[]? ProfilePictureByteArray { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public int? GroupId { get; set; }
    }
}
