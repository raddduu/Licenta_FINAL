using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class FinalGrade : IEntity
    {
        public string StudentId { get; set; }

        public int SubjectId { get; set; }

        public int Grade { get; set; }

        public ApplicationUser Student { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
    }
}
