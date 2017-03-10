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
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class My_Should
    {
        private const string AjaxContextName = "ajax";
        private const string RegularContextName = "non-ajax";

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
            var context = this.kernel.Get<HttpContextBase>(AjaxContextName);

            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetReviewsForUser(this.expectedUserId, page * pageSize, pageSize))
                         .Returns(expected);

            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);

            // Act
            sut.My(page, pageSize);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(this.expectedUserId, page * pageSize, pageSize), Times.Once);
        }

        [Test]
        public void Call_MapMethodOnceWithCorrectParams()
        {
            // Arrange
            var expected = new List<IBeerReview>();
            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(AjaxContextName);
            var mapper = this.kernel.GetMock<IMapper>();
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetReviewsForUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                         .Returns(expected);

            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);

            // Act
            sut.My(1, 1);

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(expected),
                          Times.Once);
        }

        [Test]
        public void Call_CountUserReviewsrOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(AjaxContextName);
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);

            // Act
            sut.My(1);

            // Assert
            reviewService.Verify(s => s.CountUserReviews(this.expectedUserId), Times.Once);
        }

        [Test]
        public void ReturnPartialViewWithCorrectViewModel_WhenRequestIsAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel() };
            var mapper = this.kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(It.IsAny<object>(), It.IsAny<object>()))
                  .Returns(expectedReviews);
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as PartialViewResult;
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(actualViewModel);
            Assert.AreEqual("Partial/_ReviewList", result.ViewName);
            Assert.AreEqual(expectedPage, actualViewModel.Page);
            Assert.AreEqual(expectedPageSize, actualViewModel.PageSize);
            Assert.AreEqual(expectedReviews.Count, actualViewModel.TotalCount);
        }

        [Test]
        public void ReturnViewWithCorrectViewModel_WhenRequestIsNotAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(RegularContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel() };
            var mapper = this.kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(It.IsAny<object>(), It.IsAny<object>()))
                  .Returns(expectedReviews);
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.AreEqual(expectedPage, actualViewModel.Page);
            Assert.AreEqual(expectedPageSize, actualViewModel.PageSize);
            Assert.AreEqual(expectedReviews.Count, actualViewModel.TotalCount);
        }

        [Test]
        public void SetViewModelsIsEditPropertyToTrue_WhenAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel(), new SingleReviewViewModel() };
            var mapper = this.kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(It.IsAny<object>(), It.IsAny<object>()))
                  .Returns(expectedReviews);
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            foreach (var actual in actualViewModel.Reviews)
            {
                Assert.IsTrue(actual.IsEdit);
            }
        }

        [Test]
        public void SetViewModelsIsEditPropertyToTrue_WhenRegularRequest()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.kernel.Get<ReviewsController>();
            var context = this.kernel.Get<HttpContextBase>(RegularContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel(), new SingleReviewViewModel() };
            var mapper = this.kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(It.IsAny<object>(), It.IsAny<object>()))
                  .Returns(expectedReviews);
            var reviewService = this.kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            foreach (var actual in actualViewModel.Reviews)
            {
                Assert.IsTrue(actual.IsEdit);
            }
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
                .Named(AjaxContextName);

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
                .Named(RegularContextName);

            this.kernel.Bind<ReviewsController>().ToSelf();
        }
    }
}
