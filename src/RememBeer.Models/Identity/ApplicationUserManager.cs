using System.Security.Claims;

using Microsoft.AspNet.Identity;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public virtual IdentityResult ConfirmEmail(string userId, string token)
        {
            return UserManagerExtensions.ConfirmEmail(this, userId, token);
        }

        public virtual bool IsEmailConfirmed(string userId)
        {
            return UserManagerExtensions.IsEmailConfirmed(this, userId);
        }

        public virtual ApplicationUser FindByName(string email)
        {
            return UserManagerExtensions.FindByName(this, email);
        }

        public virtual bool HasPassword(string userId)
        {
            return UserManagerExtensions.HasPassword(this, userId);
        }

        public virtual IdentityResult Create(ApplicationUser user, string password)
        {
            return UserManagerExtensions.Create(this, user, password);
        }

        public virtual ApplicationUser FindById(string userId)
        {
            return UserManagerExtensions.FindById(this, userId);
        }

        public virtual IdentityResult AddPassword(string userId, string password)
        {
            return UserManagerExtensions.AddPassword(this, userId, password);
        }

        public virtual IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return UserManagerExtensions.ChangePassword(this, userId, currentPassword, newPassword);
        }

        public virtual ClaimsIdentity CreateIdentity(ApplicationUser user, string authenticationType)
        {
            return UserManagerExtensions.CreateIdentity(this, user, authenticationType);
        }
    }
}
