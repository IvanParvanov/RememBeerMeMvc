using System;
using System.Collections.Generic;

using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IBreweryService
    {
        IEnumerable<IBrewery> GetAll();

        IEnumerable<IBrewery> GetAll<T>(int skip, int pageSize, Func<IBrewery, T> order);

        IEnumerable<IBrewery> Search(string pattern);

        IBrewery GetById(object id);

        IDataModifiedResult UpdateBrewery(int id, string name, string country, string description);

        IDataModifiedResult AddNewBeer(int breweryId, int beerTypeId, string name);

        IDataModifiedResult DeleteBeer(int beerId);
    }
}
