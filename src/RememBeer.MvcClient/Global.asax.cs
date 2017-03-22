using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using RememBeer.Data.DbContexts;

namespace RememBeer.MvcClient
{
    public class MvcApplication : HttpApplication
    {
        [ExcludeFromCodeCoverage]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapConfig.RegisterMappings();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RememBeerMeDbContext, Data.Migrations.Configuration>());
        }

        protected void Application_PreSendRequestHeaders()
        {
            this.Response.Headers.Remove("Server");
            this.Response.Headers.Remove("X-AspNet-Version");
            this.Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}
