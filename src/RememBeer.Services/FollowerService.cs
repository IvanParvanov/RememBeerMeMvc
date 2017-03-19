using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IUsersDb db;
        private readonly IDataModifiedResultFactory resultFactory;

        public FollowerService(IUsersDb db, IDataModifiedResultFactory resultFactory)
        {
            Guard.WhenArgument(db, nameof(db)).IsNull().Throw();
            Guard.WhenArgument(resultFactory, nameof(resultFactory)).IsNull().Throw();

            this.db = db;
            this.resultFactory = resultFactory;
        }

        //public async Task<IEnumerable<IApplicationUser>> GetFollowersForUsername(string username)
        //{
        //    var user = await this.db.Users.Include(u => u.Followers)
        //                         .FirstOrDefaultAsync(u => u.UserName == username);

        //    return user?.Followers;
        //}

        //public async Task<IEnumerable<IApplicationUser>> GetFollowersForUserIdAsync(string userId)
        //{
        //    var user = await this.db.Users.Include(u => u.Followers)
        //                         .FirstOrDefaultAsync(u => u.Id == userId);

        //    return user?.Followers;
        //}

        public IEnumerable<IApplicationUser> GetFollowingForUserId(string userId)
        {
            var users = this.db.Users.Where(u => u.Followers.Select(f => f.Id).Contains(userId));

            return users;
        }

        public async Task<IDataModifiedResult> AddFollowerAsync(string userId, string usernameToFollow)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return this.resultFactory.CreateDatabaseUpdateResult(false, new List<string>() { $"User {userId} not found!" });
            }

            var userToFollow = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == usernameToFollow);
            if (userToFollow == null)
            {
                return this.resultFactory.CreateDatabaseUpdateResult(false, new List<string>() { $"User {usernameToFollow} not found!" });
            }

            userToFollow.Followers.Add(user);

            return await this.CommitAsync();
        }

        public async Task<IDataModifiedResult> RemoveFollowerAsync(string userId, string usernameToRemove)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == usernameToRemove);
            if (user == null)
            {
                return this.resultFactory.CreateDatabaseUpdateResult(false, new List<string>() { $"User {usernameToRemove} not found!" });
            }

            var userToRemove = user.Followers.FirstOrDefault(u => u.Id == userId);
            user.Followers.Remove(userToRemove);

            return await this.CommitAsync();
        }

        private async Task<IDataModifiedResult> CommitAsync()
        {
            await this.db.SaveChangesAsync();
            return this.resultFactory.CreateDatabaseUpdateResult(isSuccessfull: true);
        }
    }
}

