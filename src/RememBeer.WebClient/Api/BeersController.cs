using System;
using System.Collections.Generic;
using System.Web.Http;

using RememBeer.Data.Repositories.Base;
using RememBeer.Data.Repositories.Enums;
using RememBeer.Models;
using RememBeer.Models.Dtos;

namespace RememBeer.WebClient.Api
{
    public class BeersController : ApiController
    {
        private readonly IRepository<Beer> beers;

        public BeersController(IRepository<Beer> beers)
        {
            if (beers == null)
            {
                throw new ArgumentNullException(nameof(beers));
            }

            this.beers = beers;
        }

        // GET api/Beers
        public IEnumerable<BeerDto> Get()
        {
            return this.beers.GetAll<Beer, BeerDto>(beer => beer.IsDeleted == false,
                                                    null,
                                                    null,
                                                    b => new BeerDto()
                                                         {
                                                             Id = b.Id,
                                                             Name = b.Name,
                                                             BreweryId = b.Brewery.Id,
                                                             BreweryName = b.Brewery.Name
                                                         });
        }

        // GET api/Beers?name={name}
        public IEnumerable<BeerDto> Get(string name)
        {
            return this.beers
                       .GetAll((beer) => beer.IsDeleted == false && beer.Name.StartsWith(name) || beer.Brewery.Name.StartsWith(name),
                               beer => beer.Name.StartsWith(name) ? (beer.Name == name ? 0 : 1) : 2,
                               SortOrder.Ascending,
                               b => new BeerDto()
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        BreweryId = b.Brewery.Id,
                                        BreweryName = b.Brewery.Name
                                    }
                              );
        }

        // GET api/Beers/5
        public Beer Get(int id)
        {
            return this.beers.GetById(id);
        }
    }
}
