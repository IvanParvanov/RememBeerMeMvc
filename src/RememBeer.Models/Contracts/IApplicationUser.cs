using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models.Contracts
{
    public interface IApplicationUser : IUser<string>
    {
        ClaimsIdentity GenerateUserIdentity(IApplicationUserManager manager);

        Task<ClaimsIdentity> GenerateUserIdentityAsync(IApplicationUserManager manager);

        string Email { get; set; }

        bool EmailConfirmed { get; set; }

        string PasswordHash { get; set; }

        string SecurityStamp { get; set; }

        string PhoneNumber { get; set; }

        bool PhoneNumberConfirmed { get; set; }

        bool TwoFactorEnabled { get; set; }

        DateTime? LockoutEndDateUtc { get; set; }

        bool LockoutEnabled { get; set; }

        int AccessFailedCount { get; set; }

        ICollection<BeerReview> BeerReviews { get; set; }

        ICollection<IdentityUserRole> Roles { get; }

        ICollection<IdentityUserClaim> Claims { get; }

        ICollection<IdentityUserLogin> Logins { get; }
    }
}
