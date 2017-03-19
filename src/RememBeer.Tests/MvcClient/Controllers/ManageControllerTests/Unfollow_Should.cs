using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Data.Repositories;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    [TestFixture]
    public class Unfollow_Should : ManageControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Unfollow(default(string)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase("sakji34890789kjkasddasdasdasdas ")]
        public async Task Call_FollowerServiceRemoveFollowerAsyncMethodOnceWithCorrectParams(string expectedUsername)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var dataResult = new Mock<IDataModifiedResult>();
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            followerService.Setup(s => s.RemoveFollowerAsync(It.IsAny<string>(), It.IsAny<string>()))
                           .Returns(Task.FromResult(dataResult.Object));
            // Act
            await sut.Unfollow(expectedUsername);

            // Assert
            followerService.Verify(s => s.RemoveFollowerAsync(this.expectedUserId, expectedUsername), Times.Once);
        }

        [Test]
        public async Task Return_CorrectStatusCodeResult_WhenUpdateIsNotSuccessful()
        {
            // Arrange
            const string err1 = "error1";
            const string err2 = "error2";

            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(s => s.Errors)
                      .Returns(new List<string>() { err1, err2 });
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            followerService.Setup(s => s.RemoveFollowerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(dataResult.Object));

            // Act
            var result = await sut.Unfollow("pesho") as HttpStatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
            StringAssert.Contains(err1, result.StatusDescription);
            StringAssert.Contains(err2, result.StatusDescription);
        }

        [Test]
        public async Task Return_CorrectRedirectResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ManageController>(RegularContextName);
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(s => s.Successful)
                      .Returns(true);
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            followerService.Setup(s => s.RemoveFollowerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(dataResult.Object));

            // Act
            var result = await sut.Unfollow("pesho") as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
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
