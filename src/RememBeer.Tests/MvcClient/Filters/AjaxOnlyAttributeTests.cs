using System.Net;
using System.Web;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Filters;

namespace RememBeer.Tests.MvcClient.Filters
{
    [TestFixture]
    public class AjaxOnlyAttributeTests
    {
        [Test]
        public void OnActionExecuting_Should_DoNothing_WhenContextIsAjax()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers)
                   .Returns(
                            new WebHeaderCollection
                            {
                                { "X-Requested-With", "XMLHttpRequest" }
                            });

            var sut = new AjaxOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.SetupGet(c => c.HttpContext.Request)
               .Returns(request.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            Assert.IsNull(ctx.Object.Result);
        }

        [Test]
        public void OnActionExecuting_Should_SetViewResult_WhenRequestIsNotAjax()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers)
                   .Returns(new WebHeaderCollection());
            var expectedViewData = new ViewDataDictionary();
            var controller = new Mock<ControllerBase>();
            controller.Object.ViewData = expectedViewData;

            var sut = new AjaxOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.SetupGet(c => c.HttpContext.Request)
               .Returns(request.Object);
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
        public void OnActionExecuting_Should_SetViewData_WhenRequestIsNotAjax()
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers)
                   .Returns(new WebHeaderCollection());
            var expectedViewData = new ViewDataDictionary();
            var controller = new Mock<ControllerBase>();
            controller.Object.ViewData = expectedViewData;

            var sut = new AjaxOnlyAttribute();
            var ctx = new Mock<ActionExecutingContext>();
            ctx.SetupGet(c => c.HttpContext.Request)
               .Returns(request.Object);
            ctx.Setup(c => c.Controller)
               .Returns(controller.Object);

            // Act
            sut.OnActionExecuting(ctx.Object);

            // Assert
            var actual = ctx.Object.Result as ViewResult;
            Assert.NotNull(actual);
            Assert.AreSame(expectedViewData, actual.ViewData);
            Assert.IsTrue(actual.ViewData.ContainsKey("ErrorMessage"));
            StringAssert.Contains("ensure JavaScript is enabled", (string)actual.ViewData["ErrorMessage"]);
        }
    }
}
