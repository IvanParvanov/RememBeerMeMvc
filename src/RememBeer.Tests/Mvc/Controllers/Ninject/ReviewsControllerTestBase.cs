using System.Security.Principal;
using System.Web;

using AutoMapper;

using Moq;

using Ninject.MockingKernel;

using RememBeer.Common.Services.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Controllers.Ninject.Base;

namespace RememBeer.Tests.Controllers.Ninject
{
    public class ReviewsControllerTestBase : NinjectTestBase
    {
        public override void Init()
        {
            this.Kernel.Bind<ReviewsController>().ToSelf();

            this.Kernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBeerReviewService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IBeerService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IImageUploadService>().ToMock().InSingletonScope();

            this.Kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                {
                    var request = new Mock<HttpRequestBase>();
                    var context = new Mock<HttpContextBase>();
                    context.SetupGet(x => x.Request).Returns(request.Object);
                    context.SetupGet(x => x.User.Identity).Returns((IIdentity)null);

                    return context.Object;
                })
                .InSingletonScope()
                .Named(UnauthContextName);
        }
    }
}
