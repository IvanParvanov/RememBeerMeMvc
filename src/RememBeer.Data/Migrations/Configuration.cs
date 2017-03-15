using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;

using RememBeer.Common.Constants;

namespace RememBeer.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public sealed class Configuration : DbMigrationsConfiguration<DbContexts.RememBeerMeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "RememBeer.Data.DbContexts.RememBeerMeDbContext";
        }

        protected override void Seed(DbContexts.RememBeerMeDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole(Constants.AdminRole));
            }

            context.SaveChanges();
        }
    }
}
