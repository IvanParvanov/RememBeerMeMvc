using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;

namespace RememBeer.Tests.Mvc.Controllers.Admin.UserControllerTests
{
    [TestFixture]
    public class Reviews_Should : UsersControllerTestBase
    {
        [TestCase("", 0, 0)]
        [TestCase(null, 0, 0)]
        [TestCase("sakkasjdkljasdklj2930281908ASD*()as8d", 17, 991)]
        public void Call_GetReviewsForUserMethodOnceWithCorrectParams(string expectedUserId, int expectedPage, int expectedPageSize)
        {
            // Arrange
            var sut = this.Kernel.Get<UsersController>();
            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(expectedUserId, expectedPage, expectedPageSize);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(expectedUserId, expectedPage * expectedPageSize, expectedPageSize), Times.Once);
        }

        [TestCase(-1, -1)]
        [TestCase(-50, -3)]
        [TestCase(-17, -991)]
        public void Normalize_PageValuesWhenTheyAreLessThanZero(int page, int pageSize)
        {
            // Arrange
            const int expectedPage = 0;
            const int expectedPageSize = 1;
            var sut = this.Kernel.Get<UsersController>();
            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(It.IsAny<string>(), page, pageSize);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(It.IsAny<string>(), expectedPage * expectedPageSize, expectedPageSize), Times.Once);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("sakkasjdkljasdklj2930281908ASD*()as8d")]
        public void Call_CountUserReviewsWithCorrectParamsOnce(string expectedUserId)
        {
            // Arrange
            var sut = this.Kernel.Get<UsersController>();
            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(expectedUserId);

            // Assert
            reviewService.Verify(s => s.CountUserReviews(expectedUserId), Times.Once);
        }

        [Test]
        public void Call_IMapperMapWithCorrectParamsOnce()
        {
            // Arrange
            var expectedReviews = new List<IBeerReview>();
            var sut = this.Kernel.Get<UsersController>();
            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(It.IsAny<string>()))
                         .Returns(expectedReviews);
            var mapper = this.Kernel.GetMock<IMapper>();

            // Act
            sut.Reviews(It.IsAny<string>());

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(expectedReviews), Times.Once);
        }

        [Test]
        public void Set_ViewModelsIsEditPropertyToTrue()
        {
            // Arrange
            var sut = this.Kernel.Get<UsersController>();
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };

            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(viewModels);

            // Act
            sut.Reviews(It.IsAny<string>());

            // Assert
            foreach (var model in viewModels)
            {
                Assert.IsTrue(model.IsEdit);
            }
        }

        [Test]
        public void Return_CorrectPartialView_WhenAjaxRequest()
        {
            // Arrange
            var sut = this.Kernel.Get<UsersController>();
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };
            var context = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(viewModels);

            // Act
            var result = sut.Reviews(It.IsAny<string>()) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("_ReviewList", result.ViewName);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            Assert.AreSame(viewModels, actualViewModel.Items);
        }

        [Test]
        public void Return_CorrectPartialView_WhenNotAjaxRequest()
        {
            // Arrange
            var sut = this.Kernel.Get<UsersController>();
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };
            var context = this.Kernel.Get<HttpContextBase>(RegularContextName);
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(It.IsAny<IEnumerable<IBeerReview>>()))
                  .Returns(viewModels);

            // Act
            var result = sut.Reviews(It.IsAny<string>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("My", result.ViewName);
            var actualViewModel = result.Model as PaginatedReviewsViewModel;
            Assert.IsNotNull(actualViewModel);
            Assert.AreSame(viewModels, actualViewModel.Items);
        }
    }
}
