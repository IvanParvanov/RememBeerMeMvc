using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.Identity.EntityFramework;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Models;

namespace RememBeer.Data.DbContexts
{
    [ExcludeFromCodeCoverage]
    public class RememBeerMeDbContext : IdentityDbContext<ApplicationUser>, IRememBeerMeDbContext
    {
        public RememBeerMeDbContext()
            : base("name=RememBeerMeDbContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Database.CreateIfNotExists();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual IDbSet<Beer> Beers { get; set; }

        public virtual IDbSet<BeerReview> BeerReviews { get; set; }

        public virtual IDbSet<BeerType> BeerTypes { get; set; }

        public virtual IDbSet<Brewery> Breweries { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
