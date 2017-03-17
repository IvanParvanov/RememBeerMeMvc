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
using RememBeer.Tests.Mvc.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    public class Edit_Should : ReviewsControllerTestBase
    {
        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            // Act
            var sut = this.Kernel.Get<ReviewsController>();
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(EditReviewBindingModel)), typeof(AjaxOnlyAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(1)]
        [TestCase(49979687)]
        [TestCase(314606869)]
        public void Call_ReviewServiceGetByIdMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();

            // Act
            sut.Edit(expectedId);

            // Assert
            reviewService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams_WhenReviewIsFound()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();
            var expectedBeerReview = new Mock<IBeerReview>();
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(It.IsAny<int>()))
                         .Returns(expectedBeerReview.Object);
            var mapper = this.Kernel.GetMock<IMapper>();

            // Act
            sut.Edit(It.IsAny<int>());

            // Assert
            mapper.Verify(m => m.Map<IBeerReview, SingleReviewViewModel>(expectedBeerReview.Object), Times.Once);
        }

        [Test]
        public void Return_CorrectViewResult()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();
            var beerReview = new Mock<IBeerReview>();
            var expectedViewModel = new SingleReviewViewModel();
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            var mapper = this.Kernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBeerReview, SingleReviewViewModel>(It.IsAny<IBeerReview>()))
                  .Returns(expectedViewModel);
            // Act
            var result = sut.Edit(It.IsAny<int>()) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actualViewModel = result.Model as SingleReviewViewModel;
            Assert.AreSame(expectedViewModel, actualViewModel);
        }
    }
}
