using System;
using System.Web;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Account.Login;
using RememBeer.Business.Logic.Account.Login.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Account
{
    [PresenterBinding(typeof(LoginPresenter))]
    public partial class Login : BaseMvpPage<StatelessViewModel>, ILoginView
    {
        public event EventHandler<ILoginEventArgs> OnLogin;

        public string FailureMessage
        {
            get
            {
                return this.FailureText.Text;
            }
            set
            {
                this.FailureText.Text = value;
            }
        }

        public bool ErrorMessageVisible
        {
            get
            {
                return this.ErrorMessage.Visible;
            }
            set
            {
                this.ErrorMessage.Visible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            var returnUrl = HttpUtility.UrlEncode(this.Request.QueryString["ReturnUrl"]);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                this.RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                var args = this.EventArgsFactory.CreateLoginEventArgs(this.Email.Text,
                                                                      this.Password.Text,
                                                                      this.RememberMe.Checked);
                this.OnLogin?.Invoke(this, args);
            }
        }
    }
}
