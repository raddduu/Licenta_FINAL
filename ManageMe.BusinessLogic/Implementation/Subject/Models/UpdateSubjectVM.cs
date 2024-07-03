namespace ManageMe.BusinessLogic
{
    public class UpdateSubjectVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        // public string ShortName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
