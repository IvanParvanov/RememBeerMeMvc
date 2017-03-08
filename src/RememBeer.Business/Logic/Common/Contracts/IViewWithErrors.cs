namespace RememBeer.Business.Logic.Common.Contracts
{
    public interface IViewWithErrors
    {
        string ErrorMessageText { get; set; }

        bool ErrorMessageVisible { get; set; }
    }
}
