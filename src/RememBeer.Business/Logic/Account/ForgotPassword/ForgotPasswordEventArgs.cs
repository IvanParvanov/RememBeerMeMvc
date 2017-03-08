using RememBeer.Business.Logic.Account.ForgotPassword.Contracts;

namespace RememBeer.Business.Logic.Account.ForgotPassword
{
    public class ForgotPasswordEventArgs : IForgotPasswordEventArgs
    {
        public ForgotPasswordEventArgs(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}
