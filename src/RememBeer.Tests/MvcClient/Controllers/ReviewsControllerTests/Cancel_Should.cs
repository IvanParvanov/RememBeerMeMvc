using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.ReviewsControllerTests
{
    public class Cancel_Should : ReviewsControllerTestBase
    {
        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            // Act
            var sut = this.MockingKernel.Get<ReviewsController>();
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Cancel(default(int)), typeof(AjaxOnlyAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(1)]
        [TestCase(49979687)]
        [TestCase(314606869)]
        public void Call_ReviewServiceGetByIdMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            var viewModel = new SingleReviewViewModel();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBeerReview, SingleReviewViewModel>(It.IsAny<IBeerReview>()))
                  .Returns(viewModel);

            // Act
            sut.Cancel(expectedId);

            // Assert
            reviewService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams_WhenReviewIsFound()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();
            var expectedBeerReview = new Mock<IBeerReview>();
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(It.IsAny<int>()))
                         .Returns(expectedBeerReview.Object);
            var viewModel = new SingleReviewViewModel();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBeerReview, SingleReviewViewModel>(It.IsAny<IBeerReview>()))
                  .Returns(viewModel);
            // Act
            sut.Cancel(It.IsAny<int>());

            // Assert
            mapper.Verify(m => m.Map<IBeerReview, SingleReviewViewModel>(expectedBeerReview.Object), Times.Once);
        }

        [Test]
        public void Return_CorrectViewResult()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();
            var beerReview = new Mock<IBeerReview>();
            var expectedViewModel = new SingleReviewViewModel();
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBeerReview, SingleReviewViewModel>(It.IsAny<IBeerReview>()))
                  .Returns(expectedViewModel);
            // Act
            var result = sut.Cancel(It.IsAny<int>()) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as SingleReviewViewModel;
            Assert.AreSame(expectedViewModel, actualViewModel);
        }

        [Test]
        public void Set_ModelIsEditPropertyToTrue()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();
            var beerReview = new Mock<IBeerReview>();
            var expectedViewModel = new SingleReviewViewModel();
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBeerReview, SingleReviewViewModel>(It.IsAny<IBeerReview>()))
                  .Returns(expectedViewModel);
            // Act
            sut.Cancel(It.IsAny<int>());

            // Assert
            Assert.IsTrue(expectedViewModel.IsEdit);
        }
    }
}
