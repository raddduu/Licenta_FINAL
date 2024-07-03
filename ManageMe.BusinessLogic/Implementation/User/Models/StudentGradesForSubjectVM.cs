namespace ManageMe.BusinessLogic
{
    public class StudentGradesForSubjectVM : UserMinimalInfo
    {
        public List<WeekGrade> Grades { get; set; } = new List<WeekGrade>();
    }
}
