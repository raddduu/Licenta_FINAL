using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;

namespace ManageMe.Entities
{
    public class GradingActivity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int ActivityId { get; set; }

        public virtual Activity? Activity { get; set; }

        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

        public virtual ICollection<GradingCriterion> GradingCriteria { get; set; } = new List<GradingCriterion>();
    }
}
