using RememBeer.Business.Logic.Account.Login.Contracts;

namespace RememBeer.Business.Logic.Account.Login
{
    public class LoginEventArgs : ILoginEventArgs
    {
        public LoginEventArgs(string email, string password, bool rememberMe)
        {
            this.Email = email;
            this.Password = password;
            this.RememberMe = rememberMe;
        }

        public string Email { get; }

        public string Password { get; }

        public bool RememberMe { get; }
    }
}
