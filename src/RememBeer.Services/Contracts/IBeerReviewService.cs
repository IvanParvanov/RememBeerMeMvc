using System.Collections.Generic;

using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IBeerReviewService
    {
        IEnumerable<IBeerReview> GetReviewsForUser(string userId);

        IEnumerable<IBeerReview> GetReviewsForUser(string userId, int skip, int pageSize);

        int CountUserReviews(string userId);

        IDataModifiedResult UpdateReview(IBeerReview review);

        IDataModifiedResult CreateReview(IBeerReview review);

        IDataModifiedResult DeleteReview(object id);

        IBeerReview GetById(object id);
    }
}
