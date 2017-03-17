using Microsoft.Owin.Security;

using Ninject.MockingKernel;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject
{
    public class AccountControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<AccountController>().ToSelf();

            this.Kernel.Bind<IApplicationUserManager>().ToMock().InSingletonScope();
            this.Kernel.Bind<IApplicationSignInManager>().ToMock().InSingletonScope();
            this.Kernel.Bind<IAuthenticationManager>().ToMock().InSingletonScope();
        }
    }
}
