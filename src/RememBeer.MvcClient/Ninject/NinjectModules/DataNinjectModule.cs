using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.SignalR;

using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

using RememBeer.Common.Cache;
using RememBeer.Common.Constants;
using RememBeer.Data.DbContexts;
using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Factories;

using IRequest = Ninject.Activation.IRequest;

namespace RememBeer.MvcClient.Ninject.NinjectModules
{
    [ExcludeFromCodeCoverage]
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>()
                .When(HasAncestorAssignableFrom<Hub>)
                .InTransientScope();
            this.Bind<IBeersDb>().To<RememBeerMeDbContext>()
                .When(HasAncestorAssignableFrom<Hub>)
                .InTransientScope();
            this.Bind<IUsersDb>().To<RememBeerMeDbContext>()
                .When(HasAncestorAssignableFrom<Hub>)
                .InTransientScope();

            this.Bind<IRememBeerMeDbContext>().To<RememBeerMeDbContext>()
                .When(DoesNotHaveAncestorAssignableFrom<Hub>)
                .InRequestScope();
            this.Bind<IBeersDb>().To<RememBeerMeDbContext>()
                .When(DoesNotHaveAncestorAssignableFrom<Hub>)
                .InRequestScope();
            this.Bind<IUsersDb>().To<RememBeerMeDbContext>()
                .When(DoesNotHaveAncestorAssignableFrom<Hub>)
                .InRequestScope();

            this.Rebind<IModelFactory>().To<ModelFactory>().InSingletonScope();
            this.Bind<IRankFactory>().To<ModelFactory>().InSingletonScope();

            this.Bind<IDataModifiedResultFactory>().ToFactory().InSingletonScope();

            this.Rebind<ICacheManager>()
                .ToMethod(context => new CacheManager(Constants.DefaultCacheDurationInMinutes))
                .InSingletonScope();
        }

        private static bool HasAncestorAssignableFrom<T>(IRequest request)
        {
            while (true)
            {
                if (request == null)
                {
                    return false;
                }

                if (typeof(T).IsAssignableFrom(request.Service))
                {
                    return true;
                }

                request = request.ParentRequest;
            }
        }

        private static bool DoesNotHaveAncestorAssignableFrom<T>(IRequest request)
        {
            return !HasAncestorAssignableFrom<T>(request);
        }
    }
}
