using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace RememBeer.MvcClient.Hubs.UserIdProvider
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.GetUserId();
        }
    }
}
