using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Ninject;
using Ninject.MockingKernel;

using RememBeer.Common.Services.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject.Base;

namespace RememBeer.Tests.Mvc.Controllers.Ninject
{
    public class ReviewsControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<ReviewsController>().ToSelf();

            this.Kernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBeerReviewService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IImageUploadService>().ToMock().InSingletonScope();

            this.Kernel.Bind<ReviewsController>().ToMethod(ctx =>
                                                           {
                                                               var sut = this.Kernel.Get<ReviewsController>();
                                                               var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
                                                               sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                               return sut;
                                                           })
                .Named(AjaxContextName)
                .BindingConfiguration.IsImplicit = true;

            this.Kernel.Bind<ReviewsController>().ToMethod(ctx =>
                                                           {
                                                               var sut = this.Kernel.Get<ReviewsController>();
                                                               var httpContext = this.Kernel.Get<HttpContextBase>(RegularContextName);
                                                               sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                               return sut;
                                                           })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;
        }
    }
}
