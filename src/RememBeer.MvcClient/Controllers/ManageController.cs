using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Manage;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IApplicationUserManager userManager;
        private readonly IApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        private readonly IFollowerService followerService;

        public ManageController(IApplicationUserManager userManager,
                                IApplicationSignInManager signInManager,
                                IAuthenticationManager authenticationManager,
                                IFollowerService followerService)
        {
            Guard.WhenArgument(userManager, nameof(userManager)).IsNull().Throw();
            Guard.WhenArgument(signInManager, nameof(signInManager)).IsNull().Throw();
            Guard.WhenArgument(authenticationManager, nameof(authenticationManager)).IsNull().Throw();
            Guard.WhenArgument(followerService, nameof(followerService)).IsNull().Throw();

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
            this.followerService = followerService;
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

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView(model);
            }

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

        // GET: /Manage/Follow
        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult Follow()
        {
            var userId = this.User.Identity.GetUserId();
            var followers = this.followerService.GetFollowingForUserId(userId);

            return this.PartialView("_ManageFollowing", followers);
        }

        // POST: /Manage/Follow
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public async Task<ActionResult> Follow(string username)
        {
            var userId = this.User.Identity.GetUserId();

            var result = await this.followerService.AddFollowerAsync(userId, username);
            if (result.Successful)
            {
                return this.RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound, string.Join(", ", result.Errors));
        }

        // GET: /Manage/Unfollow
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Unfollow(string username)
        {
            var userId = this.User.Identity.GetUserId();

            var result = await this.followerService.RemoveFollowerAsync(userId, username);
            if (result.Successful)
            {
                return this.RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound, string.Join(", ", result.Errors));
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
