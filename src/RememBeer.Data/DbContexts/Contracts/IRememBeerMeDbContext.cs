using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

using RememBeer.Models;

namespace RememBeer.Data.DbContexts.Contracts
{
    public interface IRememBeerMeDbContext : IDisposable
    {
        IDbSet<Beer> Beers { get; set; }

        IDbSet<BeerReview> BeerReviews { get; set; }

        IDbSet<BeerType> BeerTypes { get; set; }

        IDbSet<Brewery> Breweries { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<T> Set<T>() where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
