using System;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

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
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetPhoneNumberAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_UserManagerGetTwoFactorEnabledAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetTwoFactorEnabledAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_UserManagerGetLoginsAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            await sut.Index(null);

            // Assert
            userManager.Verify(m => m.GetLoginsAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Return_PartialViewResult_WhenRequestIsAjax()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(AjaxContextName);

            // Act
            var result = await sut.Index(null) as PartialViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public async Task Return_ViewResult_WhenRequestIsNotAjax()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);

            // Act
            var result = await sut.Index(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestCase(null, "")]
        [TestCase(ManageController.ManageMessageId.Error, "error has occurred")]
        [TestCase(ManageController.ManageMessageId.ChangePasswordSuccess, "password has been changed")]
        public async Task Set_ViewBagStatusMessageCorrectly(ManageController.ManageMessageId? msgId, string expected)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);

            // Act
            await sut.Index(msgId);

            // Assert
            StringAssert.Contains(expected, sut.ViewBag.StatusMessage);
        }

        public override void Init()
        {
            base.Init();

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers)
                                     .Returns(new WebHeaderCollection());

                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity).Returns(identity.Object);
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.User).Returns(mockedUser.Object);
                              context.SetupGet(x => x.Request)
                                     .Returns(request.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(RegularContextName);

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              request.SetupGet(x => x.Headers).Returns(
                                                                       new WebHeaderCollection
                                                                       {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                                       });

                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity).Returns(identity.Object);
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.User).Returns(mockedUser.Object);
                              context.SetupGet(x => x.Request)
                                     .Returns(request.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(AjaxContextName);

            this.MockingKernel.Bind<ManageController>().ToMethod(ctx =>
                                                          {
                                                              var sut = ctx.Kernel.Get<ManageController>();
                                                              var httpContext = ctx.Kernel.Get<HttpContextBase>(AjaxContextName);
                                                              sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);

                                                              return sut;
                                                          })
                .Named(AjaxContextName)
                .BindingConfiguration.IsImplicit = true;
        }
    }
}
