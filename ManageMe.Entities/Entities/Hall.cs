using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class Hall : IEntity
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string? AdditionalLetter { get; set; }

        public string? Name { get; set; }

        public int Capacity { get; set; }

        public int Floor { get; set; }

        public bool HasComputers { get; set; }

        public int BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
