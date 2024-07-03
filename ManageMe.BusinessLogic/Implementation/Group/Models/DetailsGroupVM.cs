using ManageMe.BusinessLogic.Implementation.Subject;
using ManageMe.Entities;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class DetailsGroupVM
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public int StudyYear { get; set; }

        public List<SubjectInfoForGroup> Subjects { get; set; } = new List<SubjectInfoForGroup>();

        public List<UserMinimalInfo> Students { get; set; } = new List<UserMinimalInfo>();
    }
}
