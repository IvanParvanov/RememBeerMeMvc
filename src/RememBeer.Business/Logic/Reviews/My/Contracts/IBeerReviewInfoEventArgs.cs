using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Reviews.My.Contracts
{
    public interface IBeerReviewInfoEventArgs
    {
        IBeerReview BeerReview { get; set; }

        byte[] Image { get; set; }
    }
}
