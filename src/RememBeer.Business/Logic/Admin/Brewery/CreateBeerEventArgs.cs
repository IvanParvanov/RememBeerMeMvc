using RememBeer.Business.Logic.Admin.Brewery.Contracts;

namespace RememBeer.Business.Logic.Admin.Brewery
{
    public class CreateBeerEventArgs : ICreateBeerEventArgs
    {
        public CreateBeerEventArgs(int id, int beerTypeId, string beerName)
        {
            this.Id = id;
            this.BeerTypeId = beerTypeId;
            this.BeerName = beerName;
        }

        public int Id { get; set; }

        public int BeerTypeId { get; }

        public string BeerName { get; }
    }
}
