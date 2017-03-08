using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

using RememBeer.Common.Cache;
using RememBeer.Common.Constants;
using RememBeer.Data.DbContexts;
using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Factories;

namespace RememBeer.CompositionRoot.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Rebind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>().InRequestScope();

            this.Rebind<IModelFactory>().To<ModelFactory>().InSingletonScope();
            this.Bind<IRankFactory>().To<ModelFactory>().InSingletonScope();

            this.Bind<IDataModifiedResultFactory>().ToFactory().InSingletonScope();

            this.Rebind<ICacheManager>()
                .ToMethod(context => new CacheManager(Constants.DefaultCacheDurationInMinutes))
                .InSingletonScope();
        }
    }
}
