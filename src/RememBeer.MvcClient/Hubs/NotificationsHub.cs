using System.Collections.Generic;
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
            var review = this.reviewService.GetLatestForUser(userId);

            var followerIds = await this.GetFollowersForUser(userId);
            var users = this.Clients.Users(followerIds);
            users.onFollowerReviewCreated(review.Id, review.User.UserName);
        }

        public async Task SendMessage(string message, string lat, string lon)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var userId = this.Context.User.Identity.GetUserId();
            var followerIds = await this.GetFollowersForUser(userId);

            var username = this.Context.User.Identity.Name;
            this.Clients.Users(followerIds).showNotification(message, username, lat, lon);
        }

        private async Task<IList<string>> GetFollowersForUser(string userId)
        {
            var followers = await this.followerService.GetFollowersForUserIdAsync(userId);
            var followerIds = followers.Select(f => f.Id).ToList();

            return followerIds;
        }
    }
}
