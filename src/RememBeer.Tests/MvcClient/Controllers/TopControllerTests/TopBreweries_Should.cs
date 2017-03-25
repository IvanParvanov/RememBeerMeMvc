using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.TopControllerTests
{
    [TestFixture]
    public class TopBreweries_Should : TopControllerTestBase
    {
        [Test]
        public void Return_CorrectView()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();

            // Act
            var actual = sut.TopBreweries();

            // Assert
            Assert.AreEqual(string.Empty, actual.ViewName);
        }
    }
}
