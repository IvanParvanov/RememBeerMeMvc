using System.Collections.Generic;

using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IBeerReviewService
    {
        IEnumerable<IBeerReview> GetReviewsForUser(string userId);

        IEnumerable<IBeerReview> GetReviewsForUser(string userId, int skip, int pageSize, string searchPattern = null);

        int CountUserReviews(string userId, string searchPattern = null);

        IDataModifiedResult UpdateReview(IBeerReview review);

        IDataModifiedResult CreateReview(IBeerReview review);

        IDataModifiedResult DeleteReview(object id);

        IBeerReview GetById(object id);

        IBeerReview GetLatestForUser(string userId);
    }
}
