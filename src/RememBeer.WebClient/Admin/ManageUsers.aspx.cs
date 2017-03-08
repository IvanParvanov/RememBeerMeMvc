using System;
using System.Web.UI.WebControls;

using RememBeer.Business.Logic.Admin.ManageUsers;
using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Admin
{
    [PresenterBinding(typeof(ManageUsersPresenter))]
    public partial class ManageUsers : BaseMvpPage<ManageUsersViewModel>, IManageUsersView
    {
        public event EventHandler<IIdentifiableEventArgs<string>> UserRemoveAdmin;

        public event EventHandler<IIdentifiableEventArgs<string>> UserDisable;

        public event EventHandler<IIdentifiableEventArgs<string>> UserEnable;

        public event EventHandler<IIdentifiableEventArgs<string>> UserMakeAdmin;

        public event EventHandler<IUserUpdateEventArgs> UserUpdate;

        public event EventHandler<ISearchEventArgs> UserSearch;

        public int CurrentPage => this.UserGridView.PageIndex;

        public int PageSize => this.UserGridView.PageSize;

        public int TotalPages { get; set; }

        public string ErrorMessageText
        {
            get
            {
                return this.Notification.ErrorText;
            }
            set
            {
                this.Notification.ErrorText = value;
            }
        }

        public bool ErrorMessageVisible
        {
            get
            {
                return this.Notification.ErrorVisible;
            }
            set
            {
                this.Notification.ErrorVisible = value;
            }
        }

        public string SuccessMessageText
        {
            get
            {
                return this.Notification.SuccessText;
            }
            set
            {
                this.Notification.SuccessText = value;
            }
        }

        public bool SuccessMessageVisible
        {
            get
            {
                return this.Notification.SuccessVisible;
            }
            set
            {
                this.Notification.SuccessVisible = value;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var pattern = this.Request.QueryString["s"];
            var args = this.EventArgsFactory.CreateSearchEventArgs(pattern);
            this.UserSearch?.Invoke(this, args);
            this.BindData();
        }

        private void BindData()
        {
            this.UserGridView.VirtualItemCount = this.TotalPages;
            this.UserGridView.DataSource = this.Model.Users;
            this.UserGridView.DataBind();
        }

        protected void UserGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                this.UserGridView.PageIndex = e.NewPageIndex;
                this.BindData();
            }
        }

        protected void UserGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            this.UserGridView.EditIndex = e.NewEditIndex;
            this.BindData();
        }

        protected void UserGridView_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.UserGridView.EditIndex = -1;
            this.BindData();
        }

        protected void UserGridView_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var userId = (string)e.NewValues["Id"];
            var email = (string)e.NewValues["Email"];
            var isConfirmed = (bool)e.NewValues["EmailConfirmed"];

            var args = this.EventArgsFactory.CreateUserUpdateEventArgs(userId, email, email, isConfirmed);
            this.UserUpdate?.Invoke(this, args);
            this.UserGridView.EditIndex = -1;
        }

        protected void OnUserCommand(object sender, CommandEventArgs e)
        {
            var userId = (string)e.CommandArgument;
            var args = this.EventArgsFactory.CreateIdentifiableEventArgs(userId);
            switch (e.CommandName)
            {
                case "EnableUser":
                    this.UserEnable?.Invoke(this, args);
                    break;
                case "DisableUser":
                    this.UserDisable?.Invoke(this, args);
                    break;
                case "MakeAdmin":
                    this.UserMakeAdmin?.Invoke(this, args);
                    break;
                case "RemoveAdmin":
                    this.UserRemoveAdmin?.Invoke(this, args);
                    break;
                default:
                    throw new ArgumentException("Invalid command!");
            }
        }

        protected void Search_OnClick(object sender, EventArgs e)
        {
            var pattern = this.SearchTb.Text;
            this.Response.Redirect("ManageUsers.aspx?s=" + pattern);
        }
    }
}
