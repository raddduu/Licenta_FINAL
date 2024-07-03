using ManageMe.Common.Interfaces;

namespace ManageMe.Entities.Entities
{
    public class Building : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<Hall> Halls { get; set; } = new List<Hall>();
    }
}
