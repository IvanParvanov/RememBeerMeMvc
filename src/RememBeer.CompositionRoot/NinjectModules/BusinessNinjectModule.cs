using System;
using System.Linq;

using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Web.Common;

using RememBeer.Business.Logic.Common;
using RememBeer.Business.Logic.MvpPresenterFactory;
using RememBeer.Common.Configuration;
using RememBeer.Common.Services;
using RememBeer.Common.Services.Contracts;
using RememBeer.Models.Identity;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Services.Contracts;
using RememBeer.Services.RankingStrategies;
using RememBeer.Services.RankingStrategies.Contracts;

using WebFormsMvp;
using WebFormsMvp.Binder;

namespace RememBeer.CompositionRoot.NinjectModules
{
    public class BusinessNinjectModule : NinjectModule
    {
        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            this.Bind<IPresenterFactory>().To<MvpPresenterFactory>().InSingletonScope();

            this.Bind<IMvpPresenterFactory>().ToFactory().InSingletonScope();

            this.Bind<ICustomEventArgsFactory>().ToFactory().InSingletonScope();

            this.Bind<IPresenter>()
                .ToMethod(GetPresenter)
                .NamedLikeFactoryMethod(
                                        (IMvpPresenterFactory factory) => factory.GetPresenter(null, null)
                                       );

            this.Rebind<IIdentityHelper>().To<IdentityHelper>().InSingletonScope();
            this.Rebind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();

            this.Bind<IRankCalculationStrategy>().To<DoubleOverallScoreStrategy>().InRequestScope();

            this.Bind<IImageUploadService>().To<CloudinaryImageUpload>();
            this.Rebind<IUserService>().To<UserService>().InRequestScope();
            this.Rebind<ITopBeersService>().To<TopBeersService>().InRequestScope();
            this.Rebind<IBeerReviewService>().To<BeerReviewService>().InRequestScope();
            this.Rebind<IBreweryService>().To<BreweryService>().InRequestScope();
        }

        private static IPresenter GetPresenter(IContext context)
        {
            var parameters = context.Parameters.ToList();

            var presenterType = (Type)parameters[0].GetValue(context, null);
            var viewInstance = (IView)parameters[1].GetValue(context, null);

            var ctorParamter = new ConstructorArgument("view", viewInstance);

            return (IPresenter)context.Kernel.Get(presenterType, ctorParamter);
        }
    }
}
