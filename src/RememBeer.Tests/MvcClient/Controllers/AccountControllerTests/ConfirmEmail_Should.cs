using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class ConfirmEmail_Should : AccountControllerTestBase
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ConfirmEmail(default(string), default(string)), typeof(AllowAnonymousAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(null, null)]
        [TestCase("sadasdasdas", null)]
        [TestCase(null, "dasklasdjkl234")]
        public async Task Return_ErrorView_WhenUserOrCodeAreNull(string userId, string code)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var result = await sut.ConfirmEmail(userId, code);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestCase("", "")]
        [TestCase("sadasdasdas", "as454655678932132@#")]
        [TestCase("321123321", "dasklasdjkl234")]
        public async Task Call_UserManagerConfirmEmailAsyncMethodOnceWithCorrectParams(string userId, string code)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.ConfirmEmailAsync(userId, code))
                       .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            await sut.ConfirmEmail(userId, code);

            // Assert
            userManager.Verify(u => u.ConfirmEmailAsync(userId, code), Times.Once);
        }

        [Test]
        public async Task Return_ConfirmEmailView_WhenConfirmationSucceeds()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await sut.ConfirmEmail("", "");

            // Assert
            Assert.AreEqual("ConfirmEmail", result.ViewName);
        }

        [Test]
        public async Task Return_ErrorView_WhenConfirmationFails()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Failed()));

            // Act
            var result = await sut.ConfirmEmail("", "");

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }
    }
}
