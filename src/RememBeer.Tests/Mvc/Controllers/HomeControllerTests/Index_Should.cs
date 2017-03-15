using NUnit.Framework;

using RememBeer.MvcClient.Controllers;

namespace RememBeer.Tests.Mvc.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnIndexView()
        {
            // Arrange
            var sut = new HomeController();
            // Act
            var result = sut.Index();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
