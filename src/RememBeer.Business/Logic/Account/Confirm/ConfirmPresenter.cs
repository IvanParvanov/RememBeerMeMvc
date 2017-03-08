using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.Confirm.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Account.Confirm
{
    public class ConfirmPresenter : UserServicePresenter<IConfirmView>
    {
        public ConfirmPresenter(IUserService userService, IConfirmView view)
            : base(userService, view)
        {
            this.View.OnSubmit += this.OnSubmit;
        }

        private void OnSubmit(object sender, IConfirmEventArgs args)
        {
            var code = args.Code;
            var userId = args.UserId;
            if (code != null && userId != null)
            {
                var result = this.UserService.ConfirmEmail(userId, code);
                if (result.Succeeded)
                {
                    this.View.SuccessPanelVisible = true;
                    return;
                }
            }

            this.View.SuccessPanelVisible = false;
            this.View.ErrorPanelVisible = true;
        }
    }
}
