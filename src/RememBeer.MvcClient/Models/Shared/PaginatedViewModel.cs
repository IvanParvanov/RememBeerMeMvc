using System.Collections.Generic;

namespace RememBeer.MvcClient.Models.Shared
{
    public class PaginatedViewModel<T> : IPaginatedViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
