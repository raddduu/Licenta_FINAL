using ManageMe.Entities;
using ManageMe.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ManageMe.BusinessLogic
{
    public class CreateChannelVM : IIdentifiable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string Title { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Description can't be longer than 200 characters")]
        public string? Description { get; set; }

        public int? SubjectId { get; set; }

        public int? AccessTypeId { get; set; }

        public string? JoinCode { get; set; } 

        public IFormFile? ChannelPicFromForm { get; set; }

        public byte[]? ChannelPicByteArray { get; set; }

        public List<string>? Roles { get; set; } = new List<string>();
    }
}
