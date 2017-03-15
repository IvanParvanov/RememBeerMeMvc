using System;

using AutoMapper;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.Admin.UserControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAdminAuthorizeAttribute()
        {
            // Act & Assert
            AttributeTester.EnsureClassHasAdminAuthorizationAttribute(typeof(UsersController));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIMapperIsNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var reviewService = new Mock<IBeerReviewService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(null, userService.Object, reviewService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIUserServiceIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(mapper.Object, null, reviewService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBeerReviewServiceIsNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(mapper.Object, userService.Object, null));
        }
    }
}
