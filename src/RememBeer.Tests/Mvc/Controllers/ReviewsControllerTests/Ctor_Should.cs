using System;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using NUnit.Framework;

using RememBeer.Common.Services.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAuthorizeAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.ClassHasAttribute(typeof(ReviewsController), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void ThrowArgumenNullException_WhenReviewServiceIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var imageUpload = new Mock<IImageUploadService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(mapper.Object, null, imageUpload.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenMapperIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var imageUpload = new Mock<IImageUploadService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(null, reviewService.Object, imageUpload.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenImageUploadServiceIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReviewsController(mapper.Object, reviewService.Object, null));
        }
    }
}
