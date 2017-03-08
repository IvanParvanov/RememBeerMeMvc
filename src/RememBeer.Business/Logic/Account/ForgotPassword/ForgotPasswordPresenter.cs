using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.ForgotPassword.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Account.ForgotPassword
{
    public class ForgotPasswordPresenter : UserServicePresenter<IForgotPasswordView>
    {
        public ForgotPasswordPresenter(IUserService userService, IForgotPasswordView view)
            : base(userService, view)
        {
            this.View.OnForgot += this.OnForgot;
        }

        private void OnForgot(object sender, IForgotPasswordEventArgs args)
        {
            var user = this.UserService.FindByName(args.Email);
            if (user == null || !this.UserService.IsEmailConfirmed(user.Id))
            {
                this.View.FailureMessage = "The user does not exist or the email is not confirmed.";
                this.View.ErrorMessageVisible = true;
                return;
            }
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send email with the code and the redirect to reset password page
            //string code = manager.GeneratePasswordResetToken(user.Id);
            //string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
            //manager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>.");
            this.View.LoginFormVisible = false;
            this.View.DisplayEmailVisible = true;
        }
    }
}
