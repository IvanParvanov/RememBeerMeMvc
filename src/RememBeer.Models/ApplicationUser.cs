using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using RememBeer.Models.Contracts;
using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public ApplicationUser()
        {
            this.BeerReviews = new HashSet<BeerReview>();
        }

        public ApplicationUser(string userName)
            : base(userName)
        {
            this.BeerReviews = new HashSet<BeerReview>();
        }

        public virtual ICollection<BeerReview> BeerReviews { get; set; }

        public virtual ClaimsIdentity GenerateUserIdentity(IApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual Task<ClaimsIdentity> GenerateUserIdentityAsync(IApplicationUserManager manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}
