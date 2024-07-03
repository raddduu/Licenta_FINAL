namespace ManageMe.BusinessLogic
{
    public class TeacherGroupVM
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int StudyYear { get; set; }

        public string StudyDomainName { get; set; }

        public List<UserMinimalInfo> Students { get; set; }

        public List<ActivityVM> ActivityList { get; set; }
    } 
}
