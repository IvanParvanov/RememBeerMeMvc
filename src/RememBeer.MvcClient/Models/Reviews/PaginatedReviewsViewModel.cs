using System.Collections.Generic;

using RememBeer.MvcClient.Models.Shared;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class PaginatedReviewsViewModel : PaginatedViewModel
    {
        public  IEnumerable<SingleReviewViewModel> Reviews { get; set; }
    }
}