using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class DetailsSubjectVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public string? Description { get; set; }

        public bool HasLaboratory { get; set; }

        public bool HasSeminary { get; set; }

        public bool HasLecture { get; set; }

        public List<Channel> Channels { get; set; } = new List<Channel>();

        public List<UserMinimalInfo> Lectors { get; set; } = new List<UserMinimalInfo>();
        public List<UserMinimalInfo> LaboratoryAssistants { get; set; } = new List<UserMinimalInfo>();
        public List<UserMinimalInfo> SeminaryAssistants { get; set; } = new List<UserMinimalInfo>();

        public List<string> StudyPlans { get; set; } = new List<string>();
    }
}
