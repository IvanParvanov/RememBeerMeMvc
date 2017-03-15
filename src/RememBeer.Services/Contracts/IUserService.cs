using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IUserService
    {
        IdentityResult RegisterUser(string username, string email, string password);

        IdentityResult ChangePassword(string userId, string currentPassword, string newPassword);

        IdentityResult ConfirmEmail(string userId, string code);

        IApplicationUser FindByName(string name);

        bool IsEmailConfirmed(string userId);

        SignInStatus PasswordSignIn(string email, string password, bool isPersistent);

        IEnumerable<IApplicationUser> PaginatedUsers(int currentPage, int pageSize, ref int total, string searchPattern = null);

        int CountUsers();

        IApplicationUser GetById(string id);

        IdentityResult UpdateUser(string id, string email, string username, bool isConfirmed);

        Task EnableUserAsync(string userId);

        Task DisableUserAsync(string userId);

        Task MakeAdminAsync(string userId);

        Task RemoveAdminAsync(string userId);
    }
}
