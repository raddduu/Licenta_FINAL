using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class GradingCriterion : IEntity
    {
        public int SubjectId { get; set; }

        public int GroupId { get; set; }
        
        public int GradingActivityId { get; set; }

        public decimal? Points { get; set; }

        public decimal? MinimumPointsRequired { get; set; }

        public bool MattersForPassingTheSubject { get; set; }

        public virtual Subject? Subject { get; set; } = null!;

        public virtual Group? Group { get; set; } = null!;

        public virtual GradingActivity? GradingActivity { get; set; } = null!;
    }
}
