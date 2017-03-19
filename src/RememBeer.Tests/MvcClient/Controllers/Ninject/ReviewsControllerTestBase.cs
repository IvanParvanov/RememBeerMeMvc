using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Ninject;
using Ninject.MockingKernel;

using RememBeer.Common.Services.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject
{
    public class ReviewsControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.MockingKernel.Bind<ReviewsController>().ToSelf();

            this.MockingKernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IBeerReviewService>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IImageUploadService>().ToMock().InSingletonScope();

            this.MockingKernel.Bind<ReviewsController>().ToMethod(ctx =>
                                                           {
                                                               var sut = ctx.Kernel.Get<ReviewsController>();
                                                               var httpContext = ctx.Kernel.Get<HttpContextBase>(AjaxContextName);
                                                               sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                               return sut;
                                                           })
                .Named(AjaxContextName)
                .BindingConfiguration.IsImplicit = true;

            this.MockingKernel.Bind<ReviewsController>().ToMethod(ctx =>
                                                           {
                                                               var sut = ctx.Kernel.Get<ReviewsController>();
                                                               var httpContext = ctx.Kernel.Get<HttpContextBase>(RegularContextName);
                                                               sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                               return sut;
                                                           })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;
        }
    }
}
