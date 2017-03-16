using System;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public NotificationsHub(IBeerReviewService service)
        {
        }

        public void Send(string name)
        {
            var userId = this.Context.User.Identity.GetUserId();

            this.Clients.User(userId).showSuccess(name + Guid.NewGuid());
        }
    }
}
