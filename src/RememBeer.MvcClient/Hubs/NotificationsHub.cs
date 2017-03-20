using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private readonly IFollowerService followerService;
        private readonly IBeerReviewService reviewService;

        public NotificationsHub(IFollowerService followerService, IBeerReviewService reviewService)
        {
            Guard.WhenArgument(followerService, nameof(followerService)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();

            this.followerService = followerService;
            this.reviewService = reviewService;
        }

        public async Task NotifyReviewCreated()
        {
            var userId = this.Context.User.Identity.GetUserId();
            var followers = await this.followerService.GetFollowersForUserIdAsync(userId);
            var review = this.reviewService.GetLatestForUser(userId);

            var followerIds = followers.Select(f => f.Id).ToList();
            this.Clients.Users(followerIds).onFollowerReviewCreated(review.Id, review.User.UserName);
        }
    }
}
