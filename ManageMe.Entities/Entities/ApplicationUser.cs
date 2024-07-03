using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace ManageMe.Entities;

public partial class ApplicationUser : IdentityUser, IEntity
{
    //public override Guid Id { get; set; }

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public byte[]? ProfilePicture { get; set; }

    public int? Number { get; set; }

    public int? EnrollmentYear { get; set; }

    public int? Salary { get; set; }

    public int? WorkHours { get; set; }

    public int? GroupId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Group? Group { get; set; } = null!;

    public virtual ICollection<ChannelRequest> ChannelRequests { get; set; } = new List<ChannelRequest>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();

    public virtual ICollection<ChannelUser> ChannelUsers { get; set; } = new List<ChannelUser>();

    public virtual ICollection<TeacherPermission> TeacherPermissions { get; set; } = new List<TeacherPermission>();

    public virtual ICollection<FinalGrade> FinalGrades { get; set; } = new List<FinalGrade>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
