namespace RememBeer.Business.Logic.Common.Contracts
{
    public interface IPaginatedView
    {
        int CurrentPage { get; }

        int PageSize { get; }

        int TotalPages { get; set; }
    }
}
