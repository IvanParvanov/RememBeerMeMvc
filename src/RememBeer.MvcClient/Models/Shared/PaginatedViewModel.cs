using System.Collections.Generic;

namespace RememBeer.MvcClient.Models.Shared
{
    public class PaginatedViewModel<T> : IPagination
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }

    public interface IPagination
    {
        int TotalCount { get; set; }

        int PageSize { get; set; }

        int CurrentPage { get; set; }
    }
}