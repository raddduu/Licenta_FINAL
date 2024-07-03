using ManageMe.Entities.Entities;
using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class HallVM
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public int Capacity { get; set; }

        public bool HasComputers { get; set; }

        public int Floor { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
