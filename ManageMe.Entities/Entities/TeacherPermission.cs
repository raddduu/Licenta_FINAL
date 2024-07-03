using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class TeacherPermission : IEntity
    {
        public string TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; } = null!;

        public virtual ApplicationUser Teacher { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
