using System;

using RememBeer.Business.Logic.Admin.UserDetails;
using RememBeer.Business.Logic.Admin.UserDetails.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Admin
{
    [PresenterBinding(typeof(UserDetailsPresenter))]
    public partial class UserDetails : BaseMvpPage<UserDetailsViewModel>, IUserDetailsView
    {
        public string ErrorMessageText
        {
            get
            {
                return this.Notifier.ErrorText;
            }

            set
            {
                this.Notifier.ErrorText = value;
            }
        }

        public bool ErrorMessageVisible
        {
            get
            {
                return this.Notifier.ErrorVisible = true;
            }

            set
            {
                this.Notifier.ErrorVisible = value;
            }
        }

        public event EventHandler<IIdentifiableEventArgs<string>> Initialized;

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = this.Request.QueryString["id"];

            var args = this.EventArgsFactory.CreateIdentifiableEventArgs(userId);
            this.Initialized?.Invoke(this, args);
            this.EditReviews.UserId = this.Model.User?.Id;
            this.UserNameLabel.Text = this.Model.User?.UserName;
        }
    }
}