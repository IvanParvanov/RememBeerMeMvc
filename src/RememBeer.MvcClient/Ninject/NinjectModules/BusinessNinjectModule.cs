using AutoMapper;

using Ninject.Modules;
using Ninject.Web.Common;

using RememBeer.Common.Configuration;
using RememBeer.Common.Services;
using RememBeer.Common.Services.Contracts;
using RememBeer.Models.Identity;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Services.Contracts;
using RememBeer.Services.RankingStrategies;
using RememBeer.Services.RankingStrategies.Contracts;

using IConfigurationProvider = RememBeer.Common.Configuration.IConfigurationProvider;

namespace RememBeer.MvcClient.Ninject.NinjectModules
{
    public class BusinessNinjectModule : NinjectModule
    {
        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            this.Rebind<IIdentityHelper>().To<IdentityHelper>().InSingletonScope();
            this.Rebind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();

            this.Bind<IRankCalculationStrategy>().To<DoubleOverallScoreStrategy>().InRequestScope<DoubleOverallScoreStrategy>();

            this.Bind<IImageUploadService>().To<CloudinaryImageUpload>();
            this.Rebind<IUserService>().To<UserService>().InRequestScope<UserService>();
            this.Rebind<ITopBeersService>().To<TopBeersService>().InRequestScope<TopBeersService>();
            this.Rebind<IBeerReviewService>().To<BeerReviewService>().InRequestScope<BeerReviewService>();
            this.Rebind<IBreweryService>().To<BreweryService>().InRequestScope<BreweryService>();

            this.Bind<IMapper>().ToMethod(m => Mapper.Instance);
        }
    }
}
