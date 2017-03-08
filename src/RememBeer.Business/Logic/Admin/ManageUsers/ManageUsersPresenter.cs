using Microsoft.AspNet.Identity;

using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Admin.ManageUsers
{
    public class ManageUsersPresenter : UserServicePresenter<IManageUsersView>
    {
        public ManageUsersPresenter(IUserService userService, IManageUsersView view)
            : base(userService, view)
        {
            this.View.UserUpdate += this.OnViewUpdateUser;
            this.View.UserMakeAdmin += this.OnViewMakeAdmin;
            this.View.UserRemoveAdmin += this.OnViewRemoveAdmin;
            this.View.UserDisable += this.OnViewDisableUser;
            this.View.UserEnable += this.OnViewEnableUser;
            this.View.UserSearch += this.OnViewUserSearch;
        }

        private void OnViewUserSearch(object sender, ISearchEventArgs e)
        {
            var currentPage = this.View.CurrentPage;
            var pageSize = this.View.PageSize;
            int total;
            var users = this.UserService.PaginatedUsers(currentPage, pageSize, out total, e?.Pattern);

            this.View.TotalPages = total;
            this.View.Model.Users = users;
        }

        private void OnViewDisableUser(object sender, IIdentifiableEventArgs<string> e)
        {
            var result = this.UserService.DisableUser(e.Id);
            this.ProcessResult(result, "User locked out!");
        }

        private void OnViewEnableUser(object sender, IIdentifiableEventArgs<string> e)
        {
            var result = this.UserService.EnableUser(e.Id);
            this.ProcessResult(result, "User has been enabled!");
        }

        private void OnViewUpdateUser(object sender, IUserUpdateEventArgs e)
        {
            var result = this.UserService.UpdateUser(e.Id, e.Email, e.UserName, e.IsConfirmed);
            this.ProcessResult(result, "User has been updated!");
        }

        private void OnViewMakeAdmin(object sender, IIdentifiableEventArgs<string> e)
        {
            var result = this.UserService.MakeAdmin(e.Id);
            this.ProcessResult(result, "User is now an administrator.");
        }

        private void OnViewRemoveAdmin(object sender, IIdentifiableEventArgs<string> e)
        {
            var result = this.UserService.RemoveAdmin(e.Id);
            this.ProcessResult(result, "Administrator removed.");
        }

        private void ProcessResult(IdentityResult result, string successMessage)
        {
            if (result.Succeeded)
            {
                this.SetSuccess(successMessage);
            }
            else
            {
                this.SetError(string.Join(", ", result.Errors));
            }
        }

        private void SetError(string error)
        {
            this.View.ErrorMessageVisible = true;
            this.View.ErrorMessageText = error;
        }

        private void SetSuccess(string message)
        {
            this.View.SuccessMessageVisible = true;
            this.View.SuccessMessageText = message;
        }
    }
}
