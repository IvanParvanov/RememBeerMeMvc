using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.TopControllerTests
{
    [TestFixture]
    public class TopBeers_Should : TopControllerTestBase
    {
        [Test]
        public void Return_CorrectView()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();

            // Act
            var actual = sut.TopBeers();

            // Assert
            Assert.AreEqual(string.Empty, actual.ViewName);
        }
    }
}

