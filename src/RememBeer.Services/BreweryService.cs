using System;
using System.Collections.Generic;
using System.Linq;

using RememBeer.Data.Repositories;
using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IEfRepository<Brewery> breweryRepository;
        private readonly IEfRepository<Beer> beerRepository;

        public BreweryService(IEfRepository<Brewery> breweryRepository, IEfRepository<Beer> beerRepository)
        {
            if (breweryRepository == null)
            {
                throw new ArgumentNullException(nameof(breweryRepository));
            }

            if (beerRepository == null)
            {
                throw new ArgumentNullException(nameof(beerRepository));
            }

            this.beerRepository = beerRepository;
            this.breweryRepository = breweryRepository;
        }

        public IEnumerable<IBrewery> GetAll()
        {
            return this.breweryRepository.GetAll();
        }

        public IEnumerable<IBrewery> GetAll<T>(int skip, int pageSize, Func<IBrewery, T> order, string searchPattern = null)
        {
            var result = this.breweryRepository.All;

            if (searchPattern != null)
            {
                result = result.Where(b => b.Name.Contains(searchPattern) || b.Country.Contains(searchPattern));
            }

            return result.OrderBy(order)
                         .Skip(skip)
                         .Take(pageSize)
                         .ToList();
        }

        public int CountAll()
        {
            return this.breweryRepository.All.Count();
        }

        public int CountAll(string pattern)
        {
            return this.breweryRepository.All.Count(x => x.Name.Contains(pattern));
        }

        public IEnumerable<IBrewery> Search(string pattern)
        {
            return this.breweryRepository.All
                       .Where(b => b.Country.Contains(pattern) || b.Name.Contains(pattern))
                       .ToList();
        }

        public IBrewery GetById(object id)
        {
            return this.breweryRepository.GetById(id);
        }

        public IDataModifiedResult UpdateBrewery(int id, string name, string country, string description)
        {
            var brewery = this.breweryRepository.GetById(id);
            brewery.Name = name;
            brewery.Country = country;
            brewery.Description = description;
            this.breweryRepository.Update(brewery);

            return this.breweryRepository.SaveChanges();
        }

        public IDataModifiedResult AddNewBeer(int breweryId, int beerTypeId, string name)
        {
            var beer = new Beer()
                       {
                           BreweryId = breweryId,
                           BeerTypeId = beerTypeId,
                           Name = name
                       };
            this.beerRepository.Add(beer);

            return this.beerRepository.SaveChanges();
        }

        public IDataModifiedResult DeleteBeer(int beerId)
        {
            var beer = this.beerRepository.GetById(beerId);
            beer.IsDeleted = true;
            return this.beerRepository.SaveChanges();
        }
    }
}
