using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.Owin;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Models.Account;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Login_Post_Should : AccountControllerTestBase
    {
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(AllowAnonymousAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void HaveRequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Login(default(LoginViewModel), default(string)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_CorrectViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var viewModel = new LoginViewModel();

            // Act
            sut.ValidateViewModel(viewModel);
            var result = await sut.Login(viewModel, It.IsAny<string>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Model as LoginViewModel;
            Assert.AreSame(viewModel, actual);
        }

        [TestCase("", "")]
        [TestCase("a@a.bg", "l;jk90i78")]
        [TestCase("aasdasdlas;kjdl;as@a.bg", "l;jk90iasd78")]
        public async Task Call_SignInManagerPasswordSignInAsyncMethodOnceWithCorrectParams(string expectedEmail, string expectedPass)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var signInManager = this.Kernel.GetMock<IApplicationSignInManager>();
            var viewModel = new LoginViewModel()
                            {
                                Email = expectedEmail,
                                Password = expectedPass,
                                RememberMe = true
                            };

            // Act
            await sut.Login(viewModel, It.IsAny<string>());

            // Assert
            signInManager.Verify(s => s.PasswordSignInAsync(expectedEmail, expectedPass, viewModel.RememberMe, true), Times.Once);
        }

        [Test]
        public async Task Return_CorrectRedirectResult_WhenUserLogsIn_WhenReturnUrlIsNull()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var signInManager = this.Kernel.GetMock<IApplicationSignInManager>();
            signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                         .Returns(Task.FromResult(SignInStatus.Success));
            var viewModel = new LoginViewModel();

            // Act
            var result = await sut.Login(viewModel, It.IsAny<string>()) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("Index", (string)result.RouteValues["action"]);
            StringAssert.Contains("Home", (string)result.RouteValues["controller"]);
        }

        [Test]
        public async Task Return_CorrectRedirectResult_WhenUserLogsIn_WhenReturnUrlIsNotNull()
        {
            // Arrange
            var expectedUrl = Guid.NewGuid().ToString();
            var sut = this.Kernel.Get<AccountController>();
            var signInManager = this.Kernel.GetMock<IApplicationSignInManager>();
            signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                         .Returns(Task.FromResult(SignInStatus.Success));
            var viewModel = new LoginViewModel();

            // Act
            var result = await sut.Login(viewModel, expectedUrl) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(expectedUrl, result.Url);
        }

        [Test]
        public async Task Return_CorrectViewResult_WhenUserIsLockedOut()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var signInManager = this.Kernel.GetMock<IApplicationSignInManager>();
            signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                         .Returns(Task.FromResult(SignInStatus.LockedOut));
            var viewModel = new LoginViewModel();

            // Act
            var result = await sut.Login(viewModel, It.IsAny<string>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("Lockout", result.ViewName);
        }

        [TestCase(SignInStatus.RequiresVerification)]
        [TestCase(SignInStatus.Failure)]
        [TestCase((SignInStatus)505500)]
        public async Task Return_CorrectViewResult_InAnyOtherCase(SignInStatus status)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();
            var signInManager = this.Kernel.GetMock<IApplicationSignInManager>();
            signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                         .Returns(Task.FromResult(status));
            var expected = new LoginViewModel();

            // Act
            var result = await sut.Login(expected, It.IsAny<string>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            var actual = result.Model as LoginViewModel;
            Assert.AreSame(expected, actual);
        }
    }
}
