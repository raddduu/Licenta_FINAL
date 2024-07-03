using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class EditChannelVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int? SubjectId { get; set; }

        public int? AccessTypeId { get; set; }

        public string? JoinCode { get; set; }

        public IFormFile? ChannelPicFromForm { get; set; }

        public byte[]? ChannelPicByteArray { get; set; }

        public List<string>? Roles { get; set; } = new List<string>();
    }
}
