using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

using RememBeer.Models;

namespace RememBeer.Data.DbContexts.Contracts
{
    public interface IRememBeerMeDbContext : IDisposable, IBeersDb, IUsersDb
    {
        IDbSet<BeerType> BeerTypes { get; set; }

        IDbSet<Brewery> Breweries { get; set; }

        IDbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }

    public interface IBeersDb : ISaveable
    {
        IDbSet<Beer> Beers { get; set; }
    }

    public interface IUsersDb : ISaveable
    {
        IDbSet<ApplicationUser> Users { get; set; }
    }

    public interface ISaveable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
