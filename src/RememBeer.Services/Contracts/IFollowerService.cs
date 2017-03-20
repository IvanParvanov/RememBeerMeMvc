using System.Collections.Generic;
using System.Threading.Tasks;

using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IFollowerService
    {
        //Task<IEnumerable<IApplicationUser>> GetFollowersForUsername(string username);

        Task<IEnumerable<IApplicationUser>> GetFollowersForUserIdAsync(string userId);

        Task<IDataModifiedResult> AddFollowerAsync(string userId, string usernameToFollow);

        Task<IDataModifiedResult> RemoveFollowerAsync(string userId, string userIdToRemove);

        IEnumerable<IApplicationUser> GetFollowingForUserId(string userId);
    }
}