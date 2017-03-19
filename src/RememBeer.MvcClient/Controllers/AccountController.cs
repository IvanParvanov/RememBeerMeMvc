using System.Threading.Tasks;
using System.Web.Mvc;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using RememBeer.Models;
using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Models.AccountModels;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IApplicationSignInManager signInManager;
        private IApplicationUserManager userManager;
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(IApplicationUserManager userManager,
                                 IApplicationSignInManager signInManager,
                                 IAuthenticationManager authenticationManager)
        {
            Guard.WhenArgument(userManager, nameof(userManager)).IsNull().Throw();
            Guard.WhenArgument(signInManager, nameof(signInManager)).IsNull().Throw();
            Guard.WhenArgument(authenticationManager, nameof(authenticationManager)).IsNull().Throw();

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    if (returnUrl != null)
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                //return this.RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    this.ModelState.AddModelError("", "Invalid login attempt.");
                    return this.View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await this.userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = this.Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await this.userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return this.RedirectToAction("Index", "Home");
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ViewResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return this.View("Error");
            }

            var result = await this.userManager.ConfirmEmailAsync(userId, code);
            return this.View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ViewResult ForgotPassword()
        {
            return this.View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await this.userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = this.Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: this.Request.Url.Scheme);
                await this.userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return this.RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ViewResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ViewResult ResetPassword(string code)
        {
            return code == null ? this.View("Error") : this.View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await this.userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            this.AddErrors(result);
            return this.View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ViewResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult LogOff()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
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
    }
}
