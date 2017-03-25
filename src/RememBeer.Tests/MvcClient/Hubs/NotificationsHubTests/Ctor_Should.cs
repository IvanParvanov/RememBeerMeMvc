using System;

using Microsoft.AspNet.SignalR;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Hubs;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Hubs.NotificationsHubTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAuthorizeAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.ClassHasAttribute(typeof(NotificationsHub), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIFollowerServiceIsNull()
        {
            // Arrange
            var reviewService = new Mock<IBeerReviewService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NotificationsHub(null, reviewService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBeerReviewServiceIsNull()
        {
            // Arrange
            var followerService = new Mock<IFollowerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new NotificationsHub(followerService.Object, null));
        }
    }
}
