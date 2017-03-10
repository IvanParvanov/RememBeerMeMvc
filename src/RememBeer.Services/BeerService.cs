using System.Collections.Generic;

using Bytes2you.Validation;

using RememBeer.Data.Repositories.Base;
using RememBeer.Data.Repositories.Enums;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class BeerService : IBeerService
    {
        private readonly IRepository<Beer> beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            Guard.WhenArgument(beerRepository, nameof(beerRepository)).IsNull().Throw();

            this.beerRepository = beerRepository;
        }

        public IEnumerable<IBeer> SearchBeers(string name)
        {
            return this.beerRepository.GetAll((beer) => beer.IsDeleted == false && beer.Name.StartsWith(name) || beer.Brewery.Name.StartsWith(name),
                               beer => beer.Name.StartsWith(name) ? (beer.Name == name ? 0 : 1) : 2,
                               SortOrder.Ascending);
        }

        public IBeer Get(int id)
        {
            return this.beerRepository.GetById(id);
        }
    }
}
