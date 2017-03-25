using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Owin.Security;

using Ninject;
using Ninject.MockingKernel;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject
{
    public class ManageControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<ManageController>().ToSelf();
            this.MockingKernel.Bind<MockedManageController>().ToSelf();

            this.MockingKernel.Bind<IApplicationUserManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IApplicationSignInManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IAuthenticationManager>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IFollowerService>().ToMock().InSingletonScope();

            this.MockingKernel.Bind<ManageController>().ToMethod(ctx =>
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
