using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Moq;

using Ninject;
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

            this.Kernel.Bind<BreweriesController>().ToMethod(ctx =>
                                                             {
                                                                 var sut = ctx.Kernel.Get<BreweriesController>();
                                                                 var httpContext = ctx.Kernel.Get<HttpContextBase>(AjaxContextName);
                                                                 sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                                 return sut;
                                                             })
                .Named(AjaxContextName)
                .BindingConfiguration.IsImplicit = true;

            this.Kernel.Bind<BreweriesController>().ToMethod(ctx =>
                                                             {
                                                                 var sut = ctx.Kernel.Get<BreweriesController>();
                                                                 var httpContext = ctx.Kernel.Get<HttpContextBase>(RegularContextName);
                                                                 sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                                 return sut;
                                                             })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;

            this.Kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers).Returns(
                                                                       new System.Net.WebHeaderCollection
                                                                       {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                                       });
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.Request).Returns(request.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(AjaxContextName);

            this.Kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection());

                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.Request).Returns(request.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(RegularContextName);
        }
    }
}
