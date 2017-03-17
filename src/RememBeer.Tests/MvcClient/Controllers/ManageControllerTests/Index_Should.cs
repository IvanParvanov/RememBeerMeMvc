using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    [TestFixture]
    public class Index_Should : ManageControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [Test]
        public async Task Call_UserManagerGetPhoneNumberAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>(RegularContextName);
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetPhoneNumberAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_UserManagerGetTwoFactorEnabledAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>(RegularContextName);
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetTwoFactorEnabledAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_UserManagerGetLoginsAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>(RegularContextName);
            var userManager = this.Kernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetLoginsAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_AuthManagerAuthenticateAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>(RegularContextName);
            var authManager = this.Kernel.GetMock<IAuthenticationManager>();

            // Act
            await sut.Index(null);

            // Assert
            authManager.Verify(m => m.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie), Times.Once);
        }

        [TestCase(null, "")]
        [TestCase(ManageController.ManageMessageId.Error, "error has occurred")]
        [TestCase(ManageController.ManageMessageId.ChangePasswordSuccess, "password has been changed")]
        public async Task Set_ViewBagStatusMessageCorrectly(ManageController.ManageMessageId? msgId, string expected)
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>(RegularContextName);

            // Act
            await sut.Index(msgId);

            // Assert
            StringAssert.Contains(expected, sut.ViewBag.StatusMessage);
        }

        public override void Init()
        {
            base.Init();

            this.Kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity).Returns(identity.Object);
                              mockedUser.Setup(u => u.IsInRole(Constants.AdminRole))
                                        .Returns(false);
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.User).Returns(mockedUser.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(RegularContextName);
        }
    }
}
