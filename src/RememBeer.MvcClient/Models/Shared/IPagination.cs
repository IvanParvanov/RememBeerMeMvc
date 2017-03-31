namespace RememBeer.MvcClient.Models.Shared
{
    public interface IPagination
    {
        int TotalCount { get; set; }

        int PageSize { get; set; }

        int CurrentPage { get; set; }
    }
}