using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class VwScheduleColor : IEntity
    {
        public int SubjectId { get; set; }
        public string TeacherId { get; set; } = null!;
        public int ActivityId { get; set; }
        public int GroupId { get; set; }
        public int HallId { get; set; }
        public int DistributionId { get; set; }
        public int ActivityFrequencyId { get; set; }
        public int Color { get; set; }
    }
}
