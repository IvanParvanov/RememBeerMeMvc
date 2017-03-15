using System.Web;

using AutoMapper;

using Moq;

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
