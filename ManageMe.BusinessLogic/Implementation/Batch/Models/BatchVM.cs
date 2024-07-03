using ManageMe.Entities;

namespace ManageMe.BusinessLogic
{
    public class BatchVM
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public bool IsForOlympiadParticipants { get; set; }

        public int StudyDomainId { get; set; }

        public int Year { get; set; }

        public string StudyDomainName { get; set; } = null!;
    }
}
