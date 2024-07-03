using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class PersonalDataUser
    {
        public string Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? UserName { get; set; }

        public DateTime? BirthDate { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        public byte[]? ProfilePictureByteArray { get; set; }

        public int? MatricolNumber { get; set; }

        public string? GroupNumber { get; set; } = null!;

        public int? StudyYear { get; set; }

        public string? StudyDomainName { get; set; }

        //public int? GroupId { get; set; }
    }
}
