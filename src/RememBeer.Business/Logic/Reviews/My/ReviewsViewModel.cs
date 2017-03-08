using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Reviews.My
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel()
        {
            this.Reviews = new HashSet<IBeerReview>();
        }

        public IEnumerable<IBeerReview> Reviews { get; set; }

        public int TotalCount { get; set; }
    }
}
