using AutoMapper;

using Ninject.MockingKernel;

using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject.Base;

namespace RememBeer.Tests.Mvc.Controllers.Ninject
{
    public class UsersControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<UsersController>().ToSelf();

            this.Kernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.Kernel.Bind<IUserService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBeerReviewService>().ToMock().InSingletonScope();
        }
    }
}
