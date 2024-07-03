namespace ManageMe.BusinessLogic
{
    public class CreateDetailVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int? ParagraphId { get; set; }

        public int? ParentDetailId { get; set; }
    }
}
