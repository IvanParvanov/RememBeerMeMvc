using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models;
using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Models.AccountModels;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class ForgotPassword_Post_Should : AccountControllerTestBase
    {
        [TestCase(typeof(AllowAnonymousAttribute))]
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ForgotPassword(default(ForgotPasswordViewModel)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_View_ModelStateIsInvalid()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var expected = new ForgotPasswordViewModel();

            // Act
            sut.ValidateViewModel(expected);
            var result = await sut.ForgotPassword(expected) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("", result.ViewName);
            var actual = result.Model as ForgotPasswordViewModel;
            Assert.NotNull(actual);
            Assert.AreSame(expected, actual);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("asdklasdjkljoiu123@jsk.bg")]
        public async Task Call_UserManagerFindByNameAsyncMethodOnceWithCorrectParams_WhenModelStateIsValid(string expectedEmail)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            var viewModel = new ForgotPasswordViewModel()
                            {
                                Email = expectedEmail
                            };

            // Act
            await sut.ForgotPassword(viewModel);

            // Assert
            userManager.Verify(x => x.FindByNameAsync(expectedEmail), Times.Once);
        }

        [Test]
        public async Task Return_ForgottenPasswordConfirmView_WhenUserIsNotFound()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var viewModel = new ForgotPasswordViewModel();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));
            // Act
            var result = await sut.ForgotPassword(viewModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            StringAssert.Contains("ForgotPasswordConfirmation", result.ViewName);
        }

        [Test]
        public async Task Return_ForgottenPasswordConfirmView_WhenUserIsNotConfirmed()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var viewModel = new ForgotPasswordViewModel();
            var user = new ApplicationUser();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user));
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(false));

            // Act
            var result = await sut.ForgotPassword(viewModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            StringAssert.Contains("ForgotPasswordConfirmation", result.ViewName);
        }

        [Test]
        public async Task Return_CorrectRedirect_WhenEverythingIsOk()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var viewModel = new ForgotPasswordViewModel();
            var user = new ApplicationUser();
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user));
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));
            // Act
            var result = await sut.ForgotPassword(viewModel) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ForgotPasswordConfirmation", result.RouteValues["action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
        }
    }
}
