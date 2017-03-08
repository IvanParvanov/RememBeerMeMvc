using System.Linq;

using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.Register.Contracts;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Account.Register
{
    public class RegisterPresenter : UserServicePresenter<IRegisterView>
    {
        private readonly IIdentityHelper identityHelper;

        public RegisterPresenter(IUserService userService, IIdentityHelper identityHelper, IRegisterView view)
            : base(userService, view)
        {
            this.View.OnRegister += this.OnRegister;
            this.identityHelper = identityHelper;
        }

        private void OnRegister(object sender, IRegisterEventArgs args)
        {
            var result = this.UserService.RegisterUser(args.UserName, args.Email, args.Password);
            if (result.Succeeded)
            {
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                var query = this.Request.QueryString["ReturnUrl"];
                var returnUrl = this.identityHelper.GetReturnUrl(query);
                this.Response.Redirect(returnUrl);
            }
            else
            {
                this.View.ErrorMessageText = result.Errors.FirstOrDefault();
            }
        }
    }
}
