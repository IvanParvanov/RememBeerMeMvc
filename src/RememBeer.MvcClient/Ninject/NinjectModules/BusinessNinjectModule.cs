using System.Diagnostics.CodeAnalysis;

using AutoMapper;

using Microsoft.AspNet.Identity;

using Ninject.Modules;
using Ninject.Web.Common;

using RememBeer.Common.Configuration;
using RememBeer.Common.Services;
using RememBeer.Common.Services.Contracts;
using RememBeer.Services;
using RememBeer.Services.Contracts;
using RememBeer.Services.RankingStrategies;
using RememBeer.Services.RankingStrategies.Contracts;

using IConfigurationProvider = RememBeer.Common.Configuration.IConfigurationProvider;

namespace RememBeer.MvcClient.Ninject.NinjectModules
{
    [ExcludeFromCodeCoverage]
    public class BusinessNinjectModule : NinjectModule
    {
        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            this.Rebind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();

            this.Bind<IRankCalculationStrategy>().To<DoubleOverallScoreStrategy>().InRequestScope();

            this.Rebind<IUserService>().To<UserService>().InRequestScope();
            this.Rebind<ITopBeersService>().To<TopBeersService>().InRequestScope();
            this.Rebind<IBeerReviewService>().To<BeerReviewService>().InRequestScope();
            this.Rebind<IBreweryService>().To<BreweryService>().InRequestScope();

            this.Bind<IMapper>().ToMethod(m => Mapper.Instance);

            this.Bind<IImageUploadService>().To<CloudinaryImageUpload>();
            this.Bind<IIdentityMessageService>().To<MailjetEmailService>();

#if DEBUG
            // Don't waste cloudinary space if in debug.
            this.Rebind<IImageUploadService>().To<DebugImageService>();
#endif
        }
    }
}
