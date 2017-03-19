using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    [TestFixture]
    public class Follow_Get_Should : ManageControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [TestCase(typeof(HttpGetAttribute))]
        [TestCase(typeof(ChildActionOnlyAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Follow(), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Call_FollowerServiceGetFollowingForUserIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var followerService = this.MockingKernel.GetMock<IFollowerService>();

            // Act
            sut.Follow();

            // Assert
            followerService.Verify(s => s.GetFollowingForUserId(this.expectedUserId), Times.Once);
        }

        [Test]
        public void Return_CorrectPartialView()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);

            // Act
            var result = sut.Follow();

            // Assert
            StringAssert.Contains("_ManageFollowing", result.ViewName);
        }

        public override void Init()
        {
            base.Init();

            this.MockingKernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var request = new Mock<HttpRequestBase>();
                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity)
                                        .Returns(identity.Object);

                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.User)
                                     .Returns(mockedUser.Object);
                              context.SetupGet(x => x.Request)
                                     .Returns(request.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(RegularContextName);
        }
    }
}
