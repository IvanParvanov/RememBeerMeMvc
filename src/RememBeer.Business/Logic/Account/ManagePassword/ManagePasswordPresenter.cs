using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.ManagePassword.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Account.ManagePassword
{
    public class ManagePasswordPresenter : UserServicePresenter<IManagePasswordView>
    {
        public ManagePasswordPresenter(IUserService userService, IManagePasswordView view)
            : base(userService, view)
        {
            this.View.ChangePassword += this.OnChangePassword;
        }

        private void OnChangePassword(object sender, IChangePasswordEventArgs args)
        {
            var userId = args.UserId;
            var result = this.UserService.ChangePassword(userId, args.CurrentPassword, args.NewPassword);
            if (result.Succeeded)
            {
                this.Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
            }
            else
            {
                this.View.AddErrors(result.Errors);
            }
        }
    }
}
