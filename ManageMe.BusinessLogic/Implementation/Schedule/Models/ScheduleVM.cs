using ManageMe.Common.Interfaces;

namespace ManageMe.BusinessLogic
{
    public class ScheduleVM : IColorable
    {
        public int SubjectId { get; set; }
        public string TeacherId { get; set; } = null!;
        public int ActivityId { get; set; }
        public int GroupId { get; set; }
        public int HallId { get; set; }
        public int DistributionId { get; set; }
        public int ActivityFrequencyId { get; set; }
        public string TeacherName { get; set; } = null!;
        public string HallName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public int GroupNumber { get; set; }
        public string ActivityName { get; set; } = null!;
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int DayOfWeek { get; set; }
        public string Color { get; set; } = null!;
        public int ColorCode { get; set; }
        public bool HasLightModeText { get; set; }
    }
}
