using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Moq;

using Ninject;
using Ninject.MockingKernel;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;

using RememBeer.Common.Services.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class My_Should
    {
        private const string AjaxContext = "ajax";
        private const string RegularContext = "non-ajax";

        private readonly string expectedUserId = Guid.NewGuid().ToString();
        private MoqMockingKernel kernel;

        [TestCase(-1, 10)]
        [TestCase(1, 50)]
        [TestCase(500, -50)]
        [TestCase(5, 200)]
        public void Call_GetReviewsForUserOnceWithCorrectParams(int page, int pageSize)
        {
            // Arrange
            var expected = new List<IBeerReview>();
            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(AjaxContext);

            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetReviewsForUser(this.expectedUserId, page * pageSize, pageSize))
                         .Returns(expected);

            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);

            // Act
            sut.My(page, pageSize);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(this.expectedUserId, page * pageSize, pageSize), Times.Once);
        }

        [TearDown]
        public void ResetMocks()
        {
            this.kernel.Reset();
        }

        [OneTimeSetUp]
        public void Init()
        {
            this.kernel = new MoqMockingKernel();
            this.kernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.kernel.Bind<IBeerReviewService>().ToMock().InSingletonScope();
            this.kernel.Bind<IBeerService>().ToMock().InSingletonScope();
            this.kernel.Bind<IImageUploadService>().ToMock().InSingletonScope();
            this.kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers).Returns(
                                                                       new System.Net.WebHeaderCollection
                                                                       {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                                       });

                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.Request).Returns(request.Object);
                              context.SetupGet(x => x.User.Identity).Returns(identity.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(AjaxContext);

            this.kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection());

                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.Request).Returns(request.Object);
                              context.SetupGet(x => x.User.Identity).Returns(identity.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(RegularContext);

            this.kernel.Bind<ReviewsController>().ToSelf();
        }
    }
}
