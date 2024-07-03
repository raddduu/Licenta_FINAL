using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class Schedule : IEntity
    {
        public int SubjectId { get; set; }

        public string TeacherId { get; set; } = null!;

        public int ActivityId { get; set; }

        public int GroupId { get; set; }

        public int HallId { get; set; }

        public int DistributionId { get; set; }

        public int ActivityFrequencyId { get; set; }
        
        public int DayOfWeek { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Duration { get; set; }

        public virtual Subject Subject { get; set; } = null!;

        public virtual ApplicationUser Teacher { get; set; } = null!;

        public virtual Activity Activity { get; set; } = null!;

        public virtual Group Group { get; set; } = null!;

        public virtual Hall Hall { get; set; } = null!;
    }
}
