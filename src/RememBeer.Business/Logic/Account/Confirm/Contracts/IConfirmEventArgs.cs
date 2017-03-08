namespace RememBeer.Business.Logic.Account.Confirm.Contracts
{
    public interface IConfirmEventArgs
    {
        string UserId { get; set; }

        string Code { get; set; }
    }
}
