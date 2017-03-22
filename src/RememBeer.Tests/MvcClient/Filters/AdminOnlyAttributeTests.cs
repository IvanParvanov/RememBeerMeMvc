using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.MvcClient.Filters;

namespace RememBeer.Tests.MvcClient.Filters
{
    [TestFixture]
    public class AdminOnlyAttributeTests
    {
        [Test]
        public void OnActionExecuting_Should_DoNothing_WhenUserIsAdmin()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated)
                   .Returns(true);
            var user = new Mock<IPrincipal>();
            user.Setup(u => u.IsInRole(Constants.AdminRole))
                .Returns(true);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.User)
                       .Returns(user.Object);
            httpContext.Setup(c => c.Request)
                       .Returns(request.Object);
            var sut = new AdminOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.Setup(c => c.HttpContext)
               .Returns(httpContext.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            Assert.IsNull(ctx.Object.Result);
        }

        [Test]
        public void OnActionExecuting_Should_SetViewResult_WhenUserIsNotLoggedIn()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated)
                   .Returns(false);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request)
                       .Returns(request.Object);
            var expectedViewData = new ViewDataDictionary();
            var controller = new Mock<ControllerBase>();
            controller.Object.ViewData = expectedViewData;
            var sut = new AdminOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.Setup(c => c.HttpContext)
               .Returns(httpContext.Object);
            ctx.Setup(c => c.Controller)
               .Returns(controller.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            var actual = ctx.Object.Result as ViewResult;
            Assert.NotNull(actual);
            Assert.AreSame("Error", actual.ViewName);
        }

        [Test]
        public void OnActionExecuting_Should_SetViewResult_WhenUserIsNotAdmin()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated)
                   .Returns(true);
            var user = new Mock<IPrincipal>();
            user.Setup(u => u.IsInRole(Constants.AdminRole))
                .Returns(false);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request)
                       .Returns(request.Object);
            httpContext.Setup(c => c.User)
                       .Returns(user.Object);
            var expectedViewData = new ViewDataDictionary();
            var controller = new Mock<ControllerBase>();
            controller.Object.ViewData = expectedViewData;
            var sut = new AdminOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.Setup(c => c.HttpContext)
               .Returns(httpContext.Object);
            ctx.Setup(c => c.Controller)
               .Returns(controller.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            var actual = ctx.Object.Result as ViewResult;
            Assert.NotNull(actual);
            Assert.AreSame("Error", actual.ViewName);
        }

        [Test]
        public void OnActionExecuting_Should_SetViewData_WhenUserIsNotAdmin()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.IsAuthenticated)
                   .Returns(true);
            var user = new Mock<IPrincipal>();
            user.Setup(u => u.IsInRole(Constants.AdminRole))
                .Returns(false);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request)
                       .Returns(request.Object);
            httpContext.Setup(c => c.User)
                       .Returns(user.Object);
            var expectedViewData = new ViewDataDictionary();
            var controller = new Mock<ControllerBase>();
            controller.Object.ViewData = expectedViewData;
            var sut = new AdminOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.Setup(c => c.HttpContext)
               .Returns(httpContext.Object);
            ctx.Setup(c => c.Controller)
               .Returns(controller.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            var actual = ctx.Object.Result as ViewResult;
            Assert.NotNull(actual);
            Assert.AreSame(expectedViewData, actual.ViewData);
            Assert.IsTrue(actual.ViewData.ContainsKey("ErrorMessage"));
            StringAssert.Contains("must be an admin", (string)actual.ViewData["ErrorMessage"]);
        }
    }
}
