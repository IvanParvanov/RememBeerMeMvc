namespace RememBeer.Business.Logic.Account.Login.Contracts
{
    public interface ILoginEventArgs
    {
        string Email { get; }

        string Password { get; }

        bool RememberMe { get; }
    }
}
