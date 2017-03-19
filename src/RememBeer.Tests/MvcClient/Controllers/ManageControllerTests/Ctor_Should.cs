using System;
using System.Web.Mvc;

using Microsoft.Owin.Security;

using Moq;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAuthorizeAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.ClassHasAttribute(typeof(ManageController), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIApplicationUserManagerIsNull()
        {
            // Arrange
            var signInManager = new Mock<IApplicationSignInManager>();
            var authManager = new Mock<IAuthenticationManager>();
            var followerService = new Mock<IFollowerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ManageController(null, signInManager.Object, authManager.Object, followerService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIApplicationSignInManagerIsNull()
        {
            // Arrange
            var userManager = new Mock<IApplicationUserManager>();
            var authManager = new Mock<IAuthenticationManager>();
            var followerService = new Mock<IFollowerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ManageController(userManager.Object, null, authManager.Object, followerService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAuthenticationManagerIsNull()
        {
            // Arrange
            var userManager = new Mock<IApplicationUserManager>();
            var signInManager = new Mock<IApplicationSignInManager>();
            var followerService = new Mock<IFollowerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ManageController(userManager.Object, signInManager.Object, null, followerService.Object));
        }
    }
}
