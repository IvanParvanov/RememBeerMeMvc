namespace RememBeer.Business.Logic.Account.Register.Contracts
{
    public interface IRegisterEventArgs
    {
        string UserName { get; set; }

        string Email { get; set; }

        string Password { get; set; }
    }
}
