using RememBeer.Business.Logic.Account.Register.Contracts;

namespace RememBeer.Business.Logic.Account.Register
{
    public class RegisterEventArgs : IRegisterEventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RegisterEventArgs(string userName, string email, string password)
        {
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
