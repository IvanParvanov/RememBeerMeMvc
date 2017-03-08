namespace RememBeer.Models.Dtos
{
    public interface IBreweryRank
    {
        decimal AveragePerBeer { get; set; }

        int TotalBeersCount { get; set; }

        string Name { get; set; }
    }
}