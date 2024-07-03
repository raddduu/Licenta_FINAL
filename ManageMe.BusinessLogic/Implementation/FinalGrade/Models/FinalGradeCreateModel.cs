namespace ManageMe.BusinessLogic
{
    public class FinalGradeCreateModel
    {
        public string StudentId { get; set; } = null!;

        public int SubjectId { get; set; }

        public int Grade { get; set; }
    }
}
