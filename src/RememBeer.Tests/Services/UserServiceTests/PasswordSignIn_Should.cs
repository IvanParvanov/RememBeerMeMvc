using Microsoft.AspNet.Identity.Owin;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class PasswordSignIn_Should : TestClassBase
    {
        [Test]
        public void CallSignInManagerPasswordSignInMethodOnce()
        {
            // Arrange
            var email = this.Fixture.Create<string>();
            var password = this.Fixture.Create<string>();
            var isPersistent = this.Fixture.Create<bool>();

            var userManager = new Mock<IApplicationUserManager>();
            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.PasswordSignIn(email, password, isPersistent);

            // Assert
            signInManager.Verify(m => m.PasswordSignIn(email, password, isPersistent), Times.Once);
        }

        [TestCase(SignInStatus.Success)]
        [TestCase(SignInStatus.Failure)]
        [TestCase(SignInStatus.LockedOut)]
        [TestCase(SignInStatus.RequiresVerification)]
        public void ReturnResultFromSignInManagerPasswordSignIn(SignInStatus expectedStatus)
        {
            // Arrange
            var email = this.Fixture.Create<string>();
            var password = this.Fixture.Create<string>();
            var isPersistent = this.Fixture.Create<bool>();

            var userManager = new Mock<IApplicationUserManager>();
            var modelFactory = new Mock<IModelFactory>();
            var signInManager = new Mock<IApplicationSignInManager>();
            signInManager.Setup(s => s.PasswordSignIn(email, password, isPersistent))
                         .Returns(expectedStatus);

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.PasswordSignIn(email, password, isPersistent);

            // Assert
            Assert.AreEqual(expectedStatus, result);
        }
    }
}
