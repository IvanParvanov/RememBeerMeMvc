namespace RememBeer.Models.Dtos
{
    public class BreweryRank : IBreweryRank
    {
        public BreweryRank(decimal averagePerBeer, int totalBeersCount, string name)
        {
            this.AveragePerBeer = averagePerBeer;
            this.TotalBeersCount = totalBeersCount;
            this.Name = name;
        }

        public decimal AveragePerBeer { get; set; }

        public int TotalBeersCount { get; set; }

        public string Name { get; set; }
    }
}
