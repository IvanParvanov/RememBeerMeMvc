using System;

using Microsoft.AspNet.Identity.Owin;

using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.Login.Contracts;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Account.Login
{
    public class LoginPresenter : UserServicePresenter<ILoginView>
    {
        private readonly IIdentityHelper identityHelper;

        public LoginPresenter(IUserService userService, IIdentityHelper identityHelper, ILoginView view)
            : base(userService, view)
        {
            if (identityHelper == null)
            {
                throw new ArgumentNullException(nameof(identityHelper));
            }

            this.identityHelper = identityHelper;
            this.View.OnLogin += this.OnLogin;
        }

        private void OnLogin(object sender, ILoginEventArgs args)
        {
            var pass = args.Password;
            var email = args.Email;
            var isPersistentLogin = args.RememberMe;

            var result = this.UserService.PasswordSignIn(email, pass, isPersistentLogin);
            switch (result)
            {
                case SignInStatus.Success:
                    var url = this.Request.QueryString["ReturnUrl"];
                    this.identityHelper.RedirectToReturnUrl(url, this.Response);
                    break;
                case SignInStatus.LockedOut:
                    this.Response.Redirect("/Account/Lockout");
                    break;
                case SignInStatus.RequiresVerification:
                    var returnUrl = $"/Account/TwoFactorAuthenticationSignIn?ReturnUrl={this.Request.QueryString["ReturnUrl"]}&RememberMe={isPersistentLogin}";
                    this.Response.Redirect(returnUrl, true);
                    break;
                case SignInStatus.Failure:
                default:
                    this.View.FailureMessage = "Username or password is incorrect!";
                    this.View.ErrorMessageVisible = true;
                    break;
            }
        }
    }
}
