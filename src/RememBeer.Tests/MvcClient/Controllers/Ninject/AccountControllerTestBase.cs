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
            this.MockingKernel.Bind<AccountController>().ToSelf();

            this.MockingKernel.Bind<IApplicationUserManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IApplicationSignInManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IAuthenticationManager>().ToMock().InSingletonScope();
        }
    }
}
