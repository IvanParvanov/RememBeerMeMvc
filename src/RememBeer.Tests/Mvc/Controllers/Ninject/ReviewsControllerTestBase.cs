using AutoMapper;

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
            this.Kernel.Bind<IBeerService>().ToMock().InSingletonScope();
            this.Kernel.Bind<IImageUploadService>().ToMock().InSingletonScope();
        }
    }
}
