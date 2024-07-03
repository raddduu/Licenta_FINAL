namespace ManageMe.BusinessLogic
{
    public class FinalGradeVM
    {
        public string StudentName { get; set; } = null!;

        public string StudentId { get; set; } = null!;

        public string SubjectName { get; set; } = null!;

        public int SubjectId { get; set; }

        public int Grade { get; set; }
    }
}
