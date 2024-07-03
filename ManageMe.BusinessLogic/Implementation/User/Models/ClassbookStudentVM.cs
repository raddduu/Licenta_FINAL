namespace ManageMe.BusinessLogic
{
    public class ClassbookStudentVM : UserMinimalInfo
    {
        public int SubjectId { get; set; }

        public decimal TotalPoints { get; set; }

        public int RoundedGrade { get; set; }

        public string PassStatus { get; set; } = null!;

        public List<ActivityGrade> ActivityGrades { get; set; } = new List<ActivityGrade>();
    }
}
