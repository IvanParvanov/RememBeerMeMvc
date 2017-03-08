using System;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Account.ManagePassword;
using RememBeer.Business.Logic.Account.ManagePassword.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Account
{
    [PresenterBinding(typeof(ManagePasswordPresenter))]
    public partial class ManagePassword : BaseMvpPage<StatelessViewModel>, IManagePasswordView
    {
        public string SuccessMessage { get; set; }

        public event EventHandler<IChangePasswordEventArgs> ChangePassword;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Render success message
                var message = this.Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    this.Form.Action = this.ResolveUrl("~/Account/Manage");
                }
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var args = this.EventArgsFactory.CreateChangePasswordEventArgs(this.CurrentPassword.Text,
                                                                               this.NewPassword.Text,
                                                                               userId);
                this.ChangePassword?.Invoke(this, args);
            }
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }
    }
}
