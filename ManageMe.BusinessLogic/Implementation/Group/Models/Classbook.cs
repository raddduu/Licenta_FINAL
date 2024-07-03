using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class Classbook
    {
        public int Id { get; set; }

        public List<StudentGradesForSubjectVM> Students { get; set; } = new List<StudentGradesForSubjectVM>();

        public string SubjectName { get; set; } = null!;

        public string GradingActivityName { get; set; } = null!;

        public int GradingActivityId { get; set; }

        public int SubjectId { get; set; }

        //public int SubjectActivityFrequencyValue { get; set; }
    }
}
