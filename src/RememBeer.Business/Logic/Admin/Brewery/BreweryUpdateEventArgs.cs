using RememBeer.Business.Logic.Admin.Brewery.Contracts;

namespace RememBeer.Business.Logic.Admin.Brewery
{
    public class BreweryUpdateEventArgs : IBreweryUpdateEventArgs
    {
        public BreweryUpdateEventArgs(int id, string description, string name, string country)
        {
            this.Id = id;
            this.Description = description;
            this.Name = name;
            this.Country = country;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}
