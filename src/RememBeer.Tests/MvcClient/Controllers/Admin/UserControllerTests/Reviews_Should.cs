using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.UserControllerTests
{
    [TestFixture]
    public class Reviews_Should : UsersControllerTestBase
    {
        [TestCase("", 0, 0, "")]
        [TestCase(null, 0, 0, null)]
        [TestCase("sakkasjdkljasdklj2930281908ASD*()as8d", 17, 991, "asjkhjkasdhqio89AS*(D&*(&(AS78y7123")]
        public void Call_GetReviewsForUserMethodOnceWithCorrectParams(string expectedUserId, int expectedPage, int expectedPageSize, string expectedSearch)
        {
            // Arrange
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(expectedUserId, expectedPage, expectedPageSize, expectedSearch);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(expectedUserId, expectedPage * expectedPageSize, expectedPageSize, expectedSearch), Times.Once);
        }

        [TestCase(-1, -1)]
        [TestCase(-50, -3)]
        [TestCase(-17, -991)]
        public void Normalize_PageValuesWhenTheyAreLessThanZero(int page, int pageSize)
        {
            // Arrange
            const int expectedPage = 0;
            const int expectedPageSize = 1;
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(It.IsAny<string>(), page, pageSize);

            // Assert
            reviewService.Verify(s => s.GetReviewsForUser(It.IsAny<string>(), expectedPage * expectedPageSize, expectedPageSize, It.IsAny<string>()), Times.Once);
        }

        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("sakkasjdkljasdklj2930281908ASD*()as8d", "sdakjh23kjhjkhk23423423")]
        public void Call_CountUserReviewsWithCorrectParamsOnce(string expectedUserId, string expectedSearch)
        {
            // Arrange
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();

            // Act
            sut.Reviews(expectedUserId, searchPattern: expectedSearch);

            // Assert
            reviewService.Verify(s => s.CountUserReviews(expectedUserId, expectedSearch), Times.Once);
        }

        [Test]
        public void Call_IMapperMapWithCorrectParamsOnce()
        {
            // Arrange
            var expectedReviews = new List<IBeerReview>();
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(It.IsAny<string>()))
                         .Returns(expectedReviews);
            var mapper = this.MockingKernel.GetMock<IMapper>();

            // Act
            sut.Reviews(It.IsAny<string>());

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(expectedReviews), Times.Once);
        }

        [Test]
        public void Set_ViewModelsIsEditPropertyToTrue()
        {
            // Arrange
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };

            var mapper = this.MockingKernel.GetMock<IMapper>();
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
            var sut = this.MockingKernel.Get<UsersController>(AjaxContextName);
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };
            var mapper = this.MockingKernel.GetMock<IMapper>();
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
            var sut = this.MockingKernel.Get<UsersController>(RegularContextName);
            var viewModels = new List<SingleReviewViewModel>()
                             {
                                 new SingleReviewViewModel(),
                                 new SingleReviewViewModel()
                             };
            var mapper = this.MockingKernel.GetMock<IMapper>();
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
