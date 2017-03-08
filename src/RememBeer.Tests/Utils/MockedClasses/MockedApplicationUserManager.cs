//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//using Microsoft.AspNet.Identity;

//using RememBeer.Data.Identity;
//using RememBeer.Data.Identity.Models;

//namespace RememBeer.Tests.Business.Account.Fakes
//{
//    public class MockedApplicationUserManager : ApplicationUserManager
//    {
//        public MockedApplicationUserManager(IUserStore<ApplicationUser> store) 
//            : base(store)
//        {
//        }

//        ///// <summary>
//        ///// Increments the access failed count for the user and if the failed access account is greater than or equal
//        ///// to the MaxFailedAccessAttempsBeforeLockout, the user will be locked out for the next DefaultAccountLockoutTimeSpan
//        ///// and the AccessFailedCount will be reset to 0. This is used for locking out the user account.
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AccessFailedAsync(string userId)
//        //{
//        //    return base.AccessFailedAsync(userId);
//        //}

//        ///// <summary>Add a user claim</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="claim"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AddClaimAsync(string userId, Claim claim)
//        //{
//        //    return base.AddClaimAsync(userId, claim);
//        //}

//        ///// <summary>Associate a login with a user</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="login"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
//        //{
//        //    return base.AddLoginAsync(userId, login);
//        //}

//        ///// <summary>
//        /////     Add a user password only if one does not already exist
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="password"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AddPasswordAsync(string userId, string password)
//        //{
//        //    return base.AddPasswordAsync(userId, password);
//        //}

//        ///// <summary>Add a user to a role</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="role"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AddToRoleAsync(string userId, string role)
//        //{
//        //    return base.AddToRoleAsync(userId, role);
//        //}

//        ///// <summary>Method to add user to multiple roles</summary>
//        ///// <param name="userId">user id</param>
//        ///// <param name="roles">list of role names</param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> AddToRolesAsync(string userId, params string[] roles)
//        //{
//        //    return base.AddToRolesAsync(userId, roles);
//        //}

//        ///// <summary>Change a user password</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="currentPassword"></param>
//        ///// <param name="newPassword"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
//        //{
//        //    return base.ChangePasswordAsync(userId, currentPassword, newPassword);
//        //}

//        ///// <summary>
//        /////     Set a user's phoneNumber with the verification token
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
//        //{
//        //    return base.ChangePhoneNumberAsync(userId, phoneNumber, token);
//        //}

//        ///// <summary>
//        /////     Returns true if the password is valid for the user
//        ///// </summary>
//        ///// <param name="user"></param>
//        ///// <param name="password"></param>
//        ///// <returns></returns>
//        //public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
//        //{
//        //    return base.CheckPasswordAsync(user, password);
//        //}

//        ///// <summary>Confirm the user's email with confirmation token</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
//        //{
//        //    return base.ConfirmEmailAsync(userId, token);
//        //}

//        ///// <summary>Create a user with no password</summary>
//        ///// <param name="user"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> CreateAsync(ApplicationUser user)
//        //{
//        //    return base.CreateAsync(user);
//        //}

//        ///// <summary>Create a user with the given password</summary>
//        ///// <param name="user"></param>
//        ///// <param name="password"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
//        //{
//        //    return base.CreateAsync(user, password);
//        //}

//        ///// <summary>Creates a ClaimsIdentity representing the user</summary>
//        ///// <param name="user"></param>
//        ///// <param name="authenticationType"></param>
//        ///// <returns></returns>
//        //public override Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
//        //{
//        //    return base.CreateIdentityAsync(user, authenticationType);
//        //}

//        ///// <summary>Delete a user</summary>
//        ///// <param name="user"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> DeleteAsync(ApplicationUser user)
//        //{
//        //    return base.DeleteAsync(user);
//        //}

//        ///// <summary>When disposing, actually dipose the store</summary>
//        ///// <param name="disposing"></param>
//        //protected override void Dispose(bool disposing)
//        //{
//        //    base.Dispose(disposing);
//        //}

//        ///// <summary>
//        /////     Return a user with the specified username and password or null if there is no match.
//        ///// </summary>
//        ///// <param name="userName"></param>
//        ///// <param name="password"></param>
//        ///// <returns></returns>
//        //public override Task<ApplicationUser> FindAsync(string userName, string password)
//        //{
//        //    return base.FindAsync(userName, password);
//        //}

//        ///// <summary>Returns the user associated with this login</summary>
//        ///// <returns></returns>
//        //public override Task<ApplicationUser> FindAsync(UserLoginInfo login)
//        //{
//        //    return base.FindAsync(login);
//        //}

//        ///// <summary>Find a user by his email</summary>
//        ///// <param name="email"></param>
//        ///// <returns></returns>
//        //public override Task<ApplicationUser> FindByEmailAsync(string email)
//        //{
//        //    return base.FindByEmailAsync(email);
//        //}

//        ///// <summary>Find a user by id</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<ApplicationUser> FindByIdAsync(string userId)
//        //{
//        //    return base.FindByIdAsync(userId);
//        //}

//        ///// <summary>Find a user by user name</summary>
//        ///// <param name="userName"></param>
//        ///// <returns></returns>
//        //public override Task<ApplicationUser> FindByNameAsync(string userName)
//        //{
//        //    return base.FindByNameAsync(userName);
//        //}

//        ///// <summary>
//        /////     Generate a code that the user can use to change their phone number to a specific number
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public override Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
//        //{
//        //    return base.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
//        //}

//        ///// <summary>Get the email confirmation token for the user</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GenerateEmailConfirmationTokenAsync(string userId)
//        //{
//        //    return base.GenerateEmailConfirmationTokenAsync(userId);
//        //}

//        ///// <summary>
//        /////     Generate a password reset token for the user using the UserTokenProvider
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GeneratePasswordResetTokenAsync(string userId)
//        //{
//        //    return base.GeneratePasswordResetTokenAsync(userId);
//        //}

//        ///// <summary>Get a token for a specific two factor provider</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="twoFactorProvider"></param>
//        ///// <returns></returns>
//        //public override Task<string> GenerateTwoFactorTokenAsync(string userId, string twoFactorProvider)
//        //{
//        //    return base.GenerateTwoFactorTokenAsync(userId, twoFactorProvider);
//        //}

//        ///// <summary>Get a user token for a specific purpose</summary>
//        ///// <param name="purpose"></param>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GenerateUserTokenAsync(string purpose, string userId)
//        //{
//        //    return base.GenerateUserTokenAsync(purpose, userId);
//        //}

//        ///// <summary>
//        /////     Returns the number of failed access attempts for the user
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<int> GetAccessFailedCountAsync(string userId)
//        //{
//        //    return base.GetAccessFailedCountAsync(userId);
//        //}

//        ///// <summary>Get a users's claims</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IList<Claim>> GetClaimsAsync(string userId)
//        //{
//        //    return base.GetClaimsAsync(userId);
//        //}

//        ///// <summary>Get a user's email</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GetEmailAsync(string userId)
//        //{
//        //    return base.GetEmailAsync(userId);
//        //}

//        ///// <summary>Returns whether lockout is enabled for the user</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> GetLockoutEnabledAsync(string userId)
//        //{
//        //    return base.GetLockoutEnabledAsync(userId);
//        //}

//        ///// <summary>
//        /////     Returns when the user is no longer locked out, dates in the past are considered as not being locked out
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
//        //{
//        //    return base.GetLockoutEndDateAsync(userId);
//        //}

//        ///// <summary>Gets the logins for a user.</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
//        //{
//        //    return base.GetLoginsAsync(userId);
//        //}

//        ///// <summary>Get a user's phoneNumber</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GetPhoneNumberAsync(string userId)
//        //{
//        //    return base.GetPhoneNumberAsync(userId);
//        //}

//        ///// <summary>Returns the roles for the user</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IList<string>> GetRolesAsync(string userId)
//        //{
//        //    return base.GetRolesAsync(userId);
//        //}

//        ///// <summary>Returns the current security stamp for a user</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<string> GetSecurityStampAsync(string userId)
//        //{
//        //    return base.GetSecurityStampAsync(userId);
//        //}

//        ///// <summary>
//        /////     Get whether two factor authentication is enabled for a user
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> GetTwoFactorEnabledAsync(string userId)
//        //{
//        //    return base.GetTwoFactorEnabledAsync(userId);
//        //}

//        ///// <summary>
//        /////     Returns a list of valid two factor providers for a user
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
//        //{
//        //    return base.GetValidTwoFactorProvidersAsync(userId);
//        //}

//        ///// <summary>Returns true if the user has a password</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> HasPasswordAsync(string userId)
//        //{
//        //    return base.HasPasswordAsync(userId);
//        //}

//        ///// <summary>
//        /////     Returns true if the user's email has been confirmed
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> IsEmailConfirmedAsync(string userId)
//        //{
//        //    return base.IsEmailConfirmedAsync(userId);
//        //}

//        ///// <summary>Returns true if the user is in the specified role</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="role"></param>
//        ///// <returns></returns>
//        //public override Task<bool> IsInRoleAsync(string userId, string role)
//        //{
//        //    return base.IsInRoleAsync(userId, role);
//        //}

//        ///// <summary>Returns true if the user is locked out</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> IsLockedOutAsync(string userId)
//        //{
//        //    return base.IsLockedOutAsync(userId);
//        //}

//        ///// <summary>
//        /////     Returns true if the user's phone number has been confirmed
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<bool> IsPhoneNumberConfirmedAsync(string userId)
//        //{
//        //    return base.IsPhoneNumberConfirmedAsync(userId);
//        //}

//        ///// <summary>
//        /////     Notify a user with a token using a specific two-factor authentication provider's Notify method
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="twoFactorProvider"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
//        //{
//        //    return base.NotifyTwoFactorTokenAsync(userId, twoFactorProvider, token);
//        //}

//        ///// <summary>
//        /////     Register a two factor authentication provider with the TwoFactorProviders mapping
//        ///// </summary>
//        ///// <param name="twoFactorProvider"></param>
//        ///// <param name="provider"></param>
//        //public override void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<ApplicationUser, string> provider)
//        //{
//        //    base.RegisterTwoFactorProvider(twoFactorProvider, provider);
//        //}

//        ///// <summary>Remove a user claim</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="claim"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> RemoveClaimAsync(string userId, Claim claim)
//        //{
//        //    return base.RemoveClaimAsync(userId, claim);
//        //}

//        ///// <summary>Remove a user from a role.</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="role"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> RemoveFromRoleAsync(string userId, string role)
//        //{
//        //    return base.RemoveFromRoleAsync(userId, role);
//        //}

//        ///// <summary>Remove user from multiple roles</summary>
//        ///// <param name="userId">user id</param>
//        ///// <param name="roles">list of role names</param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles)
//        //{
//        //    return base.RemoveFromRolesAsync(userId, roles);
//        //}

//        ///// <summary>Remove a user login</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="login"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo login)
//        //{
//        //    return base.RemoveLoginAsync(userId, login);
//        //}

//        ///// <summary>Remove a user's password</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> RemovePasswordAsync(string userId)
//        //{
//        //    return base.RemovePasswordAsync(userId);
//        //}

//        ///// <summary>Resets the access failed count for the user to 0</summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> ResetAccessFailedCountAsync(string userId)
//        //{
//        //    return base.ResetAccessFailedCountAsync(userId);
//        //}

//        ///// <summary>
//        /////     Reset a user's password using a reset password token
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="token"></param>
//        ///// <param name="newPassword"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
//        //{
//        //    return base.ResetPasswordAsync(userId, token, newPassword);
//        //}

//        ///// <summary>Send an email to the user</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="subject"></param>
//        ///// <param name="body"></param>
//        ///// <returns></returns>
//        //public override Task SendEmailAsync(string userId, string subject, string body)
//        //{
//        //    return base.SendEmailAsync(userId, subject, body);
//        //}

//        ///// <summary>Send a user a sms message</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="message"></param>
//        ///// <returns></returns>
//        //public override Task SendSmsAsync(string userId, string message)
//        //{
//        //    return base.SendSmsAsync(userId, message);
//        //}

//        ///// <summary>Set a user's email</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="email"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> SetEmailAsync(string userId, string email)
//        //{
//        //    return base.SetEmailAsync(userId, email);
//        //}

//        ///// <summary>Sets whether lockout is enabled for this user</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="enabled"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> SetLockoutEnabledAsync(string userId, bool enabled)
//        //{
//        //    return base.SetLockoutEnabledAsync(userId, enabled);
//        //}

//        ///// <summary>Sets the when a user lockout ends</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="lockoutEnd"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
//        //{
//        //    return base.SetLockoutEndDateAsync(userId, lockoutEnd);
//        //}

//        ///// <summary>Set a user's phoneNumber</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber)
//        //{
//        //    return base.SetPhoneNumberAsync(userId, phoneNumber);
//        //}

//        ///// <summary>
//        /////     Set whether a user has two factor authentication enabled
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="enabled"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> SetTwoFactorEnabledAsync(string userId, bool enabled)
//        //{
//        //    return base.SetTwoFactorEnabledAsync(userId, enabled);
//        //}

//        ///// <summary>
//        /////     Returns true if the store is an IQueryableUserStore
//        ///// </summary>
//        //public override bool SupportsQueryableUsers { get; }

//        ///// <summary>Returns true if the store is an IUserClaimStore</summary>
//        //public override bool SupportsUserClaim { get; }

//        ///// <summary>Returns true if the store is an IUserEmailStore</summary>
//        //public override bool SupportsUserEmail { get; }

//        ///// <summary>Returns true if the store is an IUserLockoutStore</summary>
//        //public override bool SupportsUserLockout { get; }

//        ///// <summary>Returns true if the store is an IUserLoginStore</summary>
//        //public override bool SupportsUserLogin { get; }

//        ///// <summary>
//        /////     Returns true if the store is an IUserPasswordStore
//        ///// </summary>
//        //public override bool SupportsUserPassword { get; }

//        ///// <summary>
//        /////     Returns true if the store is an IUserPhoneNumberStore
//        ///// </summary>
//        //public override bool SupportsUserPhoneNumber { get; }

//        ///// <summary>Returns true if the store is an IUserRoleStore</summary>
//        //public override bool SupportsUserRole { get; }

//        ///// <summary>
//        /////     Returns true if the store is an IUserSecurityStore
//        ///// </summary>
//        //public override bool SupportsUserSecurityStamp { get; }

//        ///// <summary>
//        /////     Returns true if the store is an IUserTwoFactorStore
//        ///// </summary>
//        //public override bool SupportsUserTwoFactor { get; }

//        ///// <summary>Update a user</summary>
//        ///// <param name="user"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> UpdateAsync(ApplicationUser user)
//        //{
//        //    return base.UpdateAsync(user);
//        //}

//        //protected override Task<IdentityResult> UpdatePassword(IUserPasswordStore<ApplicationUser, string> passwordStore, ApplicationUser user, string newPassword)
//        //{
//        //    return base.UpdatePassword(passwordStore, user, newPassword);
//        //}

//        ///// <summary>
//        /////     Generate a new security stamp for a user, used for SignOutEverywhere functionality
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public override Task<IdentityResult> UpdateSecurityStampAsync(string userId)
//        //{
//        //    return base.UpdateSecurityStampAsync(userId);
//        //}

//        ///// <summary>
//        /////     Returns an IQueryable of users if the store is an IQueryableUserStore
//        ///// </summary>
//        //public override IQueryable<ApplicationUser> Users { get; }

//        ///// <summary>
//        /////     Verify the code is valid for a specific user and for a specific phone number
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="token"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public override Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber)
//        //{
//        //    return base.VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber);
//        //}

//        ///// <summary>
//        /////     By default, retrieves the hashed password from the user store and calls PasswordHasher.VerifyHashPassword
//        ///// </summary>
//        ///// <param name="store"></param>
//        ///// <param name="user"></param>
//        ///// <param name="password"></param>
//        ///// <returns></returns>
//        //protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<ApplicationUser, string> store, ApplicationUser user, string password)
//        //{
//        //    return base.VerifyPasswordAsync(store, user, password);
//        //}

//        ///// <summary>
//        /////     Verify a two factor token with the specified provider
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="twoFactorProvider"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public override Task<bool> VerifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
//        //{
//        //    return base.VerifyTwoFactorTokenAsync(userId, twoFactorProvider, token);
//        //}

//        ///// <summary>Verify a user token with the specified purpose</summary>
//        ///// <param name="userId"></param>
//        ///// <param name="purpose"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public override Task<bool> VerifyUserTokenAsync(string userId, string purpose, string token)
//        //{
//        //    return base.VerifyUserTokenAsync(userId, purpose, token);
//        //}

//        ///// <summary>Determines whether the specified object is equal to the current object.</summary>
//        ///// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
//        ///// <param name="obj">The object to compare with the current object. </param>
//        //public override bool Equals(object obj)
//        //{
//        //    return base.Equals(obj);
//        //}

//        ///// <summary>Serves as the default hash function. </summary>
//        ///// <returns>A hash code for the current object.</returns>
//        //public override int GetHashCode()
//        //{
//        //    return base.GetHashCode();
//        //}

//        ///// <summary>Returns a string that represents the current object.</summary>
//        ///// <returns>A string that represents the current object.</returns>
//        //public override string ToString()
//        //{
//        //    return base.ToString();
//        //}
//    }
//}

namespace RememBeer.Tests.Utils.MockedClasses
{
}
