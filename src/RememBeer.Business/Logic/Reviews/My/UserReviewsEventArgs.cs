using RememBeer.Business.Logic.Reviews.My.Contracts;

namespace RememBeer.Business.Logic.Reviews.My
{
   public class UserReviewsEventArgs : IUserReviewsEventArgs
    {
        public UserReviewsEventArgs(int startRowIndex, int pageSize, string userId)
        {
            this.StartRowIndex = startRowIndex;
            this.PageSize = pageSize;
            this.UserId = userId;
        }

        public int StartRowIndex { get; }

        public int PageSize { get; }

        public string UserId { get; }
    }
}
