using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
    public class ForgotPassword_Post_Should : AccountControllerTestBase
    {
        private const string ExpectedUri = "http://somesite.com/account/resetpassword";

        [TestCase(typeof(AllowAnonymousAttribute))]
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ForgotPassword(default(ForgotPasswordViewModel)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_View_WhenModelStateIsInvalid()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();
            var expected = new ForgotPasswordViewModel();
            sut.InvalidateViewModel();

            // Act
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
            var sut = this.MockingKernel.Get<AccountController>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
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
            var sut = this.MockingKernel.Get<AccountController>();
            var viewModel = new ForgotPasswordViewModel();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));

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
            var sut = this.MockingKernel.Get<AccountController>(RegularContextName);
            var viewModel = new ForgotPasswordViewModel();
            var user = new Mock<ApplicationUser>();
            user.Setup(u => u.Id)
                .Returns(Guid.NewGuid().ToString);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user.Object));
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));

            // Act
            var result = await sut.ForgotPassword(viewModel) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ForgotPasswordConfirmation", result.RouteValues["action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
        }

        [Test]
        public async Task Call_UserManagerGeneratePasswordResetTokenAsyncOnceWithCorrectParams_WhenOk()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>(RegularContextName);
            var viewModel = new ForgotPasswordViewModel();
            var user = new Mock<ApplicationUser>();
            user.Setup(u => u.Id)
                .Returns(Guid.NewGuid().ToString);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user.Object));
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));

            // Act
            await sut.ForgotPassword(viewModel);

            // Assert
            userManager.Verify(m => m.GeneratePasswordResetTokenAsync(user.Object.Id), Times.Once);
        }

        [Test]
        public async Task Call_UserManagerSendEmailAsyncOnceWithCorrectParams_WhenOk()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>(RegularContextName);
            var viewModel = new ForgotPasswordViewModel();
            var user = new Mock<ApplicationUser>();
            user.Setup(u => u.Id)
                .Returns(Guid.NewGuid().ToString);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(user.Object));
            userManager.Setup(m => m.IsEmailConfirmedAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(true));

            // Act
            await sut.ForgotPassword(viewModel);

            // Assert
            userManager.Verify(m => m.SendEmailAsync(
                                                     user.Object.Id,
                                                     It.Is<string>(s => s.ToLower().Contains("reset password")),
                                                     It.Is<string>(s => s.ToLower().Contains(ExpectedUri))),
                               Times.Once);
        }

        public override void Init()
        {
            this.MockingKernel.Bind<AccountController>().ToMethod(ctx =>
                                                                  {
                                                                      var sut = ctx.Kernel.Get<AccountController>();
                                                                      var urlHelper = new Mock<UrlHelper>();
                                                                      urlHelper.Setup(h => h.Action(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                                                                               .Returns(ExpectedUri);
                                                                      sut.Url = urlHelper.Object;
                                                                      var url = new Uri(ExpectedUri);
                                                                      var request = new Mock<HttpRequestBase>();
                                                                      request.Setup(r => r.Url)
                                                                             .Returns(url);
                                                                      var httpContext = new Mock<HttpContextBase>();
                                                                      httpContext.Setup(c => c.Request)
                                                                                 .Returns(request.Object);
                                                                      sut.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), sut);

                                                                      return sut;
                                                                  })
                .Named(RegularContextName)
                .BindingConfiguration.IsImplicit = true;

            base.Init();
        }
    }
}
