using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;

namespace ManageMe.Entities;

public partial class Activity : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public virtual ICollection<TeacherPermission> TeacherPermissions { get; set; } = new List<TeacherPermission>();

    //public virtual ICollection<SubjectActivityFrequency> SubjectActivityFrequencies { get; set; } = new List<SubjectActivityFrequency>();

    public virtual ICollection<GradingActivity> GradingActivities { get; set; } = new List<GradingActivity>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
