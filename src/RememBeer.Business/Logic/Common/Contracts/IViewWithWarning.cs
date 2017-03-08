namespace RememBeer.Business.Logic.Common.Contracts
{
    public interface IViewWithWarning
    {
        string WarningMessageText { get; set; }

        bool WarningErrorMessageVisible { get; set; }
    }
}
