using AutoMapper;

using Ninject.MockingKernel;

using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject.Base;

namespace RememBeer.Tests.Mvc.Controllers.Ninject
{
    public class BreweriesControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<BreweriesController>().ToSelf();

            this.Kernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBreweryService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBeerTypesService>().ToMock().InSingletonScope();
        }
    }
}
