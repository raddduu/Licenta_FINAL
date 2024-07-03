using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class Batch : IEntity
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public bool IsForOlympiadParticipants { get; set; }

        public int StudyDomainId { get; set; }

        public int Year { get; set; }

        public virtual StudyDomain StudyDomain { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; } = null!;
    }
}
