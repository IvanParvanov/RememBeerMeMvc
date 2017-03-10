using System;

using AutoMapper;

using Moq;

using NUnit.Framework;

using RememBeer.Common.Services.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenReviewServiceIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var beerService = new Mock<IBeerService>();
            var imageUpload = new Mock<IImageUploadService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(mapper.Object, null, beerService.Object, imageUpload.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenMapperIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var beerService = new Mock<IBeerService>();
            var imageUpload = new Mock<IImageUploadService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(null, reviewService.Object, beerService.Object, imageUpload.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenBeerServiceIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var mapper = new Mock<IMapper>();
            var imageUpload = new Mock<IImageUploadService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(mapper.Object, reviewService.Object, null, imageUpload.Object));
        }
        [Test]
        public void ThrowArgumenNullException_WhenImageUploadServiceIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var mapper = new Mock<IMapper>();
            var beerService = new Mock<IBeerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(mapper.Object, reviewService.Object, beerService.Object, null));
        }
    }
}
