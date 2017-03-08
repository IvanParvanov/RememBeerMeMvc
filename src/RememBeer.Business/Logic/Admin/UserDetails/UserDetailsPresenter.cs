using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Admin.UserDetails.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Admin.UserDetails
{
    public class UserDetailsPresenter : UserServicePresenter<IUserDetailsView>
    {
        public UserDetailsPresenter(IUserService userService, IUserDetailsView view)
            : base(userService, view)
        {
            this.View.Initialized += this.OnViewInitialized;
        }

        private void OnViewInitialized(object sender, IIdentifiableEventArgs<string> e)
        {
            var user = this.UserService.GetById(e?.Id);

            if (user == null)
            {
                this.View.ErrorMessageText = "User not found!";
                this.View.ErrorMessageVisible = true;
            }
            else
            {
                this.View.Model.User = user;
                this.View.Model.Reviews = user.BeerReviews;
            }
        }
    }
}
