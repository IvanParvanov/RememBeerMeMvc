using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models;
using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Models.Account;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class ResetPassword_Post_Should : AccountControllerTestBase
    {
        [TestCase(typeof(AllowAnonymousAttribute))]
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ResetPassword(default(ResetPasswordViewModel)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_ViewWithModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel();

            // Act
            sut.ValidateViewModel(expected);
            var result = await sut.ResetPassword(expected) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("", result.ViewName);
            var actual = result.Model as ResetPasswordViewModel;
            Assert.AreSame(expected, actual);
        }

        [TestCase("asdasd")]
        [TestCase("asdasdasdljkjlk123")]
        [TestCase("")]
        public async Task Call_UserManagerFindByNameAsyncOnceWithCorrectParams(string expectedEmail)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel()
                           {
                               Email = expectedEmail
                           };
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.ResetPassword(expected);

            // Assert
            userManager.Verify(m => m.FindByNameAsync(expectedEmail), Times.Once);
        }

        [Test]
        public async Task NotCall_UserManagerResetPassword_WhenUserIsNotFound()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.ResetPassword(expected);

            // Assert
            userManager.Verify(m => m.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Return_CorrectRedirect_WhenUserIsNotFound()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel();

            // Act
            var result = await sut.ResetPassword(expected) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ResetPasswordConfirmation", result.RouteValues["action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
        }

        [TestCase(null, null)]
        [TestCase("sadasd221312asd123asdas", "sdadasdas123123dasdasasd")]
        [TestCase("sadasdasdasdas", "sdadasdasdasasd")]
        public async Task Call_UserManagerResetPassword_WhenUserIsFound(string expectedCode, string expectedPass)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel()
                           {
                               Code = expectedCode,
                               Password = expectedPass
                           };
            var user = new ApplicationUser();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user));
            userManager.Setup(m => m.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            await sut.ResetPassword(expected);

            // Assert
            userManager.Verify(m => m.ResetPasswordAsync(user.Id, expectedCode, expectedPass), Times.Once);
        }

        [Test]
        public async Task Return_RedirectToResetPasswordConfirmation_WhenResetSucceeds()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel();
            var user = new ApplicationUser();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user));
            userManager.Setup(m => m.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await sut.ResetPassword(expected) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ResetPasswordConfirmation", result.RouteValues["action"]);
        }

        [Test]
        public async Task Return_View_WhenResetFails()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ResetPasswordViewModel();
            var user = new ApplicationUser();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user));
            userManager.Setup(m => m.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Failed("")));

            // Act
            var result = await sut.ResetPassword(expected) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}
