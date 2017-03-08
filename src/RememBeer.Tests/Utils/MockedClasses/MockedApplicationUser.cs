using RememBeer.Models;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedApplicationUser : ApplicationUser
    {
        ///// <summary>
        /////     Used to record failures for the purposes of lockout
        ///// </summary>
        //public override int AccessFailedCount { get; set; }

        ///// <summary>Navigation property for user claims</summary>
        //public override ICollection<IdentityUserClaim> Claims { get; }

        /// <summary>Email</summary>
        public override string Email { get; set; }

        ///// <summary>True if the email is confirmed, default is false</summary>
        //public override bool EmailConfirmed { get; set; }

        /// <summary>User ID (Primary Key)</summary>
        public override string Id { get; set; }

        ///// <summary>Is lockout enabled for this user</summary>
        //public override bool LockoutEnabled { get; set; }

        ///// <summary>
        /////     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        ///// </summary>
        //public override DateTime? LockoutEndDateUtc { get; set; }

        ///// <summary>Navigation property for user logins</summary>
        //public override ICollection<IdentityUserLogin> Logins { get; }

        ///// <summary>The salted/hashed form of the user password</summary>
        //public override string PasswordHash { get; set; }

        ///// <summary>PhoneNumber for the user</summary>
        //public override string PhoneNumber { get; set; }

        ///// <summary>
        /////     True if the phone number is confirmed, default is false
        ///// </summary>
        //public override bool PhoneNumberConfirmed { get; set; }

        ///// <summary>Navigation property for user roles</summary>
        //public override ICollection<IdentityUserRole> Roles { get; }

        ///// <summary>
        /////     A random value that should change whenever a users credentials have changed (password changed, login removed)
        ///// </summary>
        //public override string SecurityStamp { get; set; }

        ///// <summary>Is two factor enabled for the user</summary>
        //public override bool TwoFactorEnabled { get; set; }

        /// <summary>User name</summary>
        public override string UserName { get; set; }
    }
}
