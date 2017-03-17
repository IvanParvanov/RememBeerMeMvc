using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Owin.Security;

using Ninject;
using Ninject.MockingKernel;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject
{
    public class ManageControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<ManageController>().ToSelf();
            this.Kernel.Bind<MockedManageController>().ToSelf();

            this.Kernel.Bind<IApplicationUserManager>().ToMock().InSingletonScope();
            this.Kernel.Bind<IApplicationSignInManager>().ToMock().InSingletonScope();
            this.Kernel.Bind<IAuthenticationManager>().ToMock().InSingletonScope();

            this.Kernel.Bind<ManageController>().ToMethod(ctx =>
                                                          {
                                                              var sut = ctx.Kernel.Get<ManageController>();
                                                              var httpContext = ctx.Kernel.Get<HttpContextBase>(RegularContextName);
                                                              sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                              return sut;
                                                          })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;
        }
    }
}
