using System.Collections.Generic;

namespace RememBeer.MvcClient.Models.Shared
{
    public interface IPaginatedViewModel<T> : IPagination
    {
        IEnumerable<T> Items { get; set; }
    }
}