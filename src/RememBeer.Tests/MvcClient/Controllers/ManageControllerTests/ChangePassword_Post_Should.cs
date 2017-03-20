using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models;
using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    [TestFixture]
    public class ChangePassword_Post_Should : ManageControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ChangePassword(default(ChangePasswordViewModel)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_ViewWithModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>();
            var expected = new ChangePasswordViewModel();
            sut.InvalidateViewModel();

            // Act
            var result = await sut.ChangePassword(expected) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
            var actual = result.Model as ChangePasswordViewModel;
            Assert.AreSame(expected, actual);
        }

        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("sdasdasdasasd1231289asd7uiasdd", "asdjkh123789687969876213asjhasd41")]
        public async Task Call_UserManagerChangePasswordAsyncMethodOnceWithCorrectParams(string expectedOld, string expectedNew)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var expected = new ChangePasswordViewModel()
                           {
                               NewPassword = expectedNew,
                               OldPassword = expectedOld
                           };
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.ChangePasswordAsync(this.expectedUserId, expectedOld, expectedNew))
                       .Returns(Task.FromResult(IdentityResult.Failed()));
            // Act
            await sut.ChangePassword(expected);

            // Assert
            userManager.Verify(m => m.ChangePasswordAsync(this.expectedUserId, expectedOld, expectedNew), Times.Once);
        }

        [Test]
        public async Task Return_ViewWithModel_WhenChangePasswordFails()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var expected = new ChangePasswordViewModel();

            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Failed("asd")));
            // Act
            var result = await sut.ChangePassword(expected) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
            var actual = result.Model as ChangePasswordViewModel;
            Assert.AreSame(expected, actual);
        }

        [Test]
        public async Task Call_UserManagerFindByIdAsyncMethodWithCorrectParamsOnce_WhenChangePassSucceeds()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var expected = new Mock<ChangePasswordViewModel>().Object;

            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));
            
            // Act
            await sut.ChangePassword(expected);

            // Assert
            userManager.Verify(m => m.FindByIdAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_SignInManagerSignInAsynccMethodWithCorrectParamsOnce_WhenUserManagerFindsUser()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var viewModel = new Mock<ChangePasswordViewModel>().Object;
            var expectedUser = new Mock<ApplicationUser>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));
            userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(expectedUser.Object));
            var signInManager = this.MockingKernel.GetMock<IApplicationSignInManager>();

            // Act
            await sut.ChangePassword(viewModel);

            // Assert
            signInManager.Verify(s => s.SignInAsync(expectedUser.Object, false, false), Times.Once);
        }

        [Test]
        public async Task Return_CorrectRedirectResult_WhenChangePasswordSucceeds()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var expected = new Mock<ChangePasswordViewModel>().Object;

            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            userManager.Setup(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await sut.ChangePassword(expected) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(ManageController.ManageMessageId.ChangePasswordSuccess, result.RouteValues["message"]);
        }

        public override void Init()
        {
            base.Init();

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                {
                    var identity = new Mock<ClaimsIdentity>();
                    identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                            .Returns(new Claim("sa", this.expectedUserId));

                    var mockedUser = new Mock<IPrincipal>();
                    mockedUser.Setup(u => u.Identity).Returns(identity.Object);

                    var context = new Mock<HttpContextBase>();
                    context.SetupGet(x => x.User).Returns(mockedUser.Object);

                    return context.Object;
                })
                .InSingletonScope()
                .Named(RegularContextName);
        }
    }
}
