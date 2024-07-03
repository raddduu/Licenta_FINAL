using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class IndexGroupVM
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public int StudyYear { get; set; }

        public string StudyDomainName { get; set; }

        //public List<UserMinimalInfo> Students { get; set; } = new List<UserMinimalInfo>();
        //public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
