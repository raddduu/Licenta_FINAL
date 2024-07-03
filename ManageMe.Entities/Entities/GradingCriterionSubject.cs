using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class GradingCriterionSubject : IEntity
    {
        public int SubjectId { get; set; }

        public int GroupId { get; set; }

        public decimal? MinimumPointsRequired { get; set; }

        public decimal? BonusPoints { get; set; }

        public bool BonusPointsMattersForPassingSubject { get; set; }

        public virtual Subject Subject { get; set; } = null!;

        public virtual Group Group { get; set; } = null!;
    }
}
