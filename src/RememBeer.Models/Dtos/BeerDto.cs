namespace RememBeer.Models.Dtos
{
    public class BeerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BreweryId { get; set; }

        public string BreweryName { get; set; }
    }
}
