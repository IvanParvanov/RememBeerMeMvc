using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Reviews.Common.ViewModels
{
    public class BeerReviewViewModel
    {
        public virtual IBeerReview Review { get; set; }
    }
}
