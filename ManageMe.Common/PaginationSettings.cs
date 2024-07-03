namespace ManageMe.Common
{
    public class PaginationSettings
    {
        public int PageSize { get; set; }

        public double LastPage { get; set; }

        public int CurrentPage { get; set; }

        public string SearchString { get; set; }

        public string PaginationBaseUrl { get; set; }
    }
}
