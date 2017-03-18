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

        //
        // GET: /Manage/ChangePassword
        public ViewResult ChangePassword()
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

            var userId = this.User.Identity.GetUserId();
            var result = await this.userManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);

            return this.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

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
            Error
        }
    }
}