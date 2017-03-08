using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Reviews.My.Contracts
{
    public interface IUserReviewsEventArgs : IPaginationEventArgs
    {
        string UserId { get; }
    }
}
