using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class VwTeacherGroupSubjectActivities : IEntity
    {
        public string TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int ActivityTypeId { get; set; }

        public int GroupId { get; set; }

        public string SubjectName { get; set; }

        public string ActivityName { get; set; }
    }
}
