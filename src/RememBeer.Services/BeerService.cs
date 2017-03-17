using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bytes2you.Validation;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeersDb db;

        public BeerService(IBeersDb db)
        {
            Guard.WhenArgument(db, nameof(db)).IsNull().Throw();

            this.db = db;
        }

        public IEnumerable<IBeer> SearchBeers(string name)
        {
            return this.db.Beers.Where(beer => beer.IsDeleted == false && beer.Name.Contains(name) || beer.Brewery.Name.StartsWith(name))
                       .OrderBy(beer => beer.Name.StartsWith(name) ? (beer.Name == name ? 0 : 1) : 2)
                       .Include(b => b.Brewery)
                       .ToList();
        }

        public IBeer Get(int id)
        {
            return this.db.Beers.Find(id);
        }
    }
}
