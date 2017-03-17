using NUnit.Framework;

using RememBeer.MvcClient.Controllers;

namespace RememBeer.Tests.Mvc.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Error_Should
    {
        [Test]
        public void ReturnErrorView()
        {
            // Arrange
            var sut = new HomeController();

            // Act
            var result = sut.Error();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Error", result.ViewName);
        }
    }
}
