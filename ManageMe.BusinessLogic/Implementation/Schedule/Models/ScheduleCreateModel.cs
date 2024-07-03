namespace ManageMe.BusinessLogic
{
    public class ScheduleCreateModel
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
    }
}
