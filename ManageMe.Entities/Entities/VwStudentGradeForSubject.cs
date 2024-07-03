using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class VwStudentGradeForSubject : IEntity
    {
        public string StudentId { get; set; } 

        public int SubjectId { get; set; } 

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public int GroupId { get; set; }

        public decimal TotalPoints { get; set; }

        public int TotalPointsRounded { get; set; }

        public int GradingActivityId { get; set; }

        public decimal GradingActivityTotalPoints { get; set; }

        public bool GradingActivityPassed { get; set; }

        //public string Status { get; set; } = null!;
    }
}
