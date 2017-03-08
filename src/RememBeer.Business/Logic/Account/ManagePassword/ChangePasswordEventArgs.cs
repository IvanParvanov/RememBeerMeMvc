using RememBeer.Business.Logic.Account.ManagePassword.Contracts;

namespace RememBeer.Business.Logic.Account.ManagePassword
{
    public class ChangePasswordEventArgs : IChangePasswordEventArgs
    {
        public ChangePasswordEventArgs(string currentPassword, string newPassword, string userId)
        {
            this.CurrentPassword = currentPassword;
            this.NewPassword = newPassword;
            this.UserId = userId;
        }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string UserId { get; set; }
    }
}
