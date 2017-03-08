namespace RememBeer.Business.Logic.Common.EventArgs.Contracts
{
    public interface IPaginationEventArgs
    {
        int StartRowIndex { get; }

        int PageSize { get; }
    }
}
