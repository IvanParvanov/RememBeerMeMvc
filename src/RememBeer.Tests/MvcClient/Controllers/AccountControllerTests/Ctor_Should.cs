using System;
using System.Web.Mvc;

using Microsoft.Owin.Security;

using Moq;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAuthorizeAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.ClassHasAttribute(typeof(AccountController), typeof(AuthorizeAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIApplicationUserManagerIsNull()
        {
            // Arrange
            var signInManager = new Mock<IApplicationSignInManager>();
            var authenticationManager = new Mock<IAuthenticationManager>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(null, signInManager.Object, authenticationManager.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIApplicationSignInManagerIsNull()
        {
            // Arrange
            var userManager = new Mock<IApplicationUserManager>();
            var authenticationManager = new Mock<IAuthenticationManager>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(userManager.Object, null, authenticationManager.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIAuthenticationManagersNull()
        {
            // Arrange
            var signInManager = new Mock<IApplicationSignInManager>();
            var userManager = new Mock<IApplicationUserManager>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(userManager.Object, signInManager.Object, null));
        }
    }
}
