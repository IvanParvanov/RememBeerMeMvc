using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services.Contracts;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.Services
{
    public class UserService : IUserService
    {
        private readonly IModelFactory factory;
        private readonly IApplicationSignInManager signInManager;
        private readonly IApplicationUserManager userManager;

        public UserService(IApplicationUserManager userManager,
                           IApplicationSignInManager signInManager,
                           IModelFactory factory)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (signInManager == null)
            {
                throw new ArgumentNullException(nameof(signInManager));
            }

            this.signInManager = signInManager;
            this.userManager = userManager;
            this.factory = factory;
        }

        public IdentityResult RegisterUser(string username, string email, string password)
        {
            var user = (ApplicationUser)this.factory.CreateApplicationUser(username, email);
            var result = this.userManager.Create(user, password);

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //string code = manager.GenerateEmailConfirmationToken(user.Id);
            //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
            //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
            return result;
        }

        public IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var result = this.userManager.ChangePassword(userId, currentPassword, newPassword);
            if (result.Succeeded)
            {
                var user = this.userManager.FindById(userId);
                this.signInManager.SignIn(user, false, false);
            }

            return result;
        }

        public IdentityResult ConfirmEmail(string userId, string code)
        {
            return this.userManager.ConfirmEmail(userId, code);
        }

        public SignInStatus PasswordSignIn(string email, string password, bool isPersistent)
        {
            return this.signInManager.PasswordSignIn(email, password, isPersistent);
        }

        public IApplicationUser FindByName(string name)
        {
            return this.userManager.FindByName(name);
        }

        public bool IsEmailConfirmed(string userId)
        {
            return this.userManager.IsEmailConfirmed(userId);
        }

        public IEnumerable<IApplicationUser> PaginatedUsers(int currentPage,
                                                            int pageSize,
                                                            out int totalCount,
                                                            string searchPattern = null)
        {
            var result = this.userManager.Users;

            if (searchPattern != null)
            {
                result = result.Where(u => u.UserName.Contains(searchPattern));
            }

            totalCount = result.Count();

            return result.OrderBy(u => u.UserName)
                         .Skip(currentPage * pageSize)
                         .Take(pageSize)
                         .ToList();
        }

        public int CountUsers()
        {
            return this.userManager.Users.Count();
        }

        public async Task<IdentityResult> DisableUserAsync(string userId)
        {
            var result = await this.userManager.UpdateSecurityStampAsync(userId);
            if (!result.Succeeded)
            {
                return result;
            }

            return await this.userManager.SetLockoutEndDateAsync(userId, DateTimeOffset.MaxValue);
        }

        public Task<IdentityResult> EnableUserAsync(string userId)
        {
            return this.userManager.SetLockoutEndDateAsync(userId, DateTimeOffset.MinValue);
        }

        public IdentityResult MakeAdmin(string userId)
        {
            return this.userManager.AddToRoleAsync(userId, Constants.AdminRole).Result;
        }

        public IdentityResult RemoveAdmin(string userId)
        {
            return this.userManager.RemoveFromRoleAsync(userId, Constants.AdminRole).Result;
        }

        public IApplicationUser GetById(string id)
        {
            return this.userManager.FindById(id);
        }

        public IdentityResult UpdateUser(string id, string email, string username, bool isConfirmed)
        {
            var user = this.userManager.FindById(id);
            user.Email = email;
            user.UserName = username;
            user.EmailConfirmed = isConfirmed;

            return this.userManager.UpdateAsync(user).Result;
        }
    }
}
