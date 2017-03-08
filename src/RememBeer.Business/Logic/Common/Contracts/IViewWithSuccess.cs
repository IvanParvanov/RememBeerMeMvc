namespace RememBeer.Business.Logic.Common.Contracts
{
    public interface IViewWithSuccess
    {
        string SuccessMessageText { get; set; }

        bool SuccessMessageVisible { get; set; }
    }
}
