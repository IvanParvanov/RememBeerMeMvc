namespace RememBeer.MvcClient.Models.Shared
{
    public class PaginatedViewModel
    {
        public int TotalCount { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}