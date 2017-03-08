using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;

namespace RememBeer.Business.Logic.Admin.ManageUsers
{
    public class UserUpdateEventArgs : IUserUpdateEventArgs
    {
        public UserUpdateEventArgs(string id, string email, string userName, bool isConfirmed)
        {
            this.Id = id;
            this.Email = email;
            this.UserName = userName;
            this.IsConfirmed = isConfirmed;
        }

        public string Id { get; set; }

        public string Email { get; }

        public string UserName { get; }

        public bool IsConfirmed { get; }
    }
}
