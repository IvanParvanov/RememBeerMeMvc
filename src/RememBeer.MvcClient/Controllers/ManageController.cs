using System.Threading.Tasks;
using System.Web.Mvc;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Models.Manage;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class ManageController : Controller
    {
        private IApplicationUserManager userManager;
        private readonly IApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;

        public ManageController(IApplicationUserManager userManager, IApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            Guard.WhenArgument(userManager, nameof(userManager)).IsNull().Throw();
            Guard.WhenArgument(signInManager, nameof(signInManager)).IsNull().Throw();
            Guard.WhenArgument(authenticationManager, nameof(authenticationManager)).IsNull().Throw();

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var userId = this.User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await this.userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await this.userManager.GetLoginsAsync(userId),
                BrowserRemembered = await this.authenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return this.View(model);
        }

        ////
        //// POST: /Manage/RemoveLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        //{
        //    ManageMessageId? message;
        //    var result = await this.userManager.RemoveLoginAsync(this.User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        //    if (result.Succeeded)
        //    {
        //        var user = await this.userManager.FindByIdAsync(this.User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        message = ManageMessageId.RemoveLoginSuccess;
        //    }
        //    else
        //    {
        //        message = ManageMessageId.Error;
        //    }
        //    return this.RedirectToAction("ManageLogins", new { Message = message });
        //}

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.userManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.userManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.userManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.userManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }

            this.AddErrors(result);

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        ////
        //// GET: /Manage/ManageLogins
        //public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        //{
        //    this.ViewBag.StatusMessage =
        //        message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : "";
        //    var user = await this.userManager.FindByIdAsync(this.User.Identity.GetUserId());
        //    if (user == null)
        //    {
        //        return this.View("Error");
        //    }
        //    var userLogins = await this.userManager.GetLoginsAsync(this.User.Identity.GetUserId());
        //    var otherLogins = this.AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
        //    this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
        //    return this.View(new ManageLoginsViewModel
        //    {
        //        CurrentLogins = userLogins,
        //        OtherLogins = otherLogins
        //    });
        //}

        ////
        //// POST: /Manage/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new AccountController.ChallengeResult(provider, this.Url.Action("LinkLoginCallback", "Manage"), this.User.Identity.GetUserId());
        //}

        ////
        //// GET: /Manage/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, this.User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await this.userManager.AddLoginAsync(this.User.Identity.GetUserId(), loginInfo.Login);
        //    return result.Succeeded ? this.RedirectToAction("ManageLogins") : this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = this.userManager.FindById(this.User.Identity.GetUserId());

            return user?.PasswordHash != null;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }

#endregion
    }
}