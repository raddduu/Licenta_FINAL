using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class UpdateUserVM
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[]? ProfilePictureByteArray { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}