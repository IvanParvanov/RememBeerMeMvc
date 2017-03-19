using Ninject.MockingKernel;

using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject
{
    public class TopControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<TopController>().ToSelf();

            this.MockingKernel.Bind<ITopBeersService>().ToMock().InSingletonScope();
        }
    }
}
