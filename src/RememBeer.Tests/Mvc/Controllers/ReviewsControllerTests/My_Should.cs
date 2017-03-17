using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class My_Should : ReviewsControllerTestBase

    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [TestCase(0, 0)]
        [TestCase(1, 50)]
        [TestCase(500, 17)]
        public void Call_GetReviewsForUserOnceWithCorrectParams(int page, int pageSize)
        {
            // Arrange
            var expected = new List<IBeerReview>();
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);

            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetReviewsForUser(this.expectedUserId, page * pageSize, pageSize))
                         .Returns(expected);

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
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var mapper = this.Kernel.GetMock<IMapper>();
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetReviewsForUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                         .Returns(expected);

            // Act
            sut.My();

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(expected),
                          Times.Once);
        }

        [Test]
        public void Call_CountUserReviewsrOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();

            // Act
            sut.My();

            // Assert
            reviewService.Verify(s => s.CountUserReviews(this.expectedUserId), Times.Once);
        }

        [Test]
        public void ReturnPartialViewWithCorrectViewModel_WhenRequestIsAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel() };
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(expectedReviews);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            Assert.AreEqual("_ReviewList", result.ViewName);
            Assert.AreEqual(expectedPage, actualViewModel.CurrentPage);
            Assert.AreEqual(expectedPageSize, actualViewModel.PageSize);
            Assert.AreEqual(expectedReviews.Count, actualViewModel.TotalCount);
        }

        [Test]
        public void ReturnViewWithCorrectViewModel_WhenRequestIsNotAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.Kernel.Get<ReviewsController>(RegularContextName);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel() };
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(expectedReviews);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            Assert.AreEqual(string.Empty, result.ViewName);
            Assert.AreEqual(expectedPage, actualViewModel.CurrentPage);
            Assert.AreEqual(expectedPageSize, actualViewModel.PageSize);
            Assert.AreEqual(expectedReviews.Count, actualViewModel.TotalCount);
        }

        [Test]
        public void SetViewModelsIsEditPropertyToTrue_WhenAjax()
        {
            // Arrange
            var expectedPageSize = 50;
            var expectedPage = 11;

            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel(), new SingleReviewViewModel() };
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(expectedReviews);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            foreach (var actual in actualViewModel.Items)
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

            var sut = this.Kernel.Get<ReviewsController>(RegularContextName);
            var expectedReviews = new List<SingleReviewViewModel>() { new SingleReviewViewModel(), new SingleReviewViewModel() };
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(expectedReviews);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedReviews.Count);

            // Act
            var result = sut.My(expectedPage, expectedPageSize) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            foreach (var actual in actualViewModel.Items)
            {
                Assert.IsTrue(actual.IsEdit);
            }
        }

        public override void Init()
        {
            this.Kernel.Bind<HttpContextBase>()
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

            this.Kernel.Bind<HttpContextBase>()
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

            base.Init();
        }
    }
}
