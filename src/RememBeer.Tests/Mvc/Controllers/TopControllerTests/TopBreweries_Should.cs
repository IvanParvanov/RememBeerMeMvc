using System.Collections.Generic;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;

namespace RememBeer.Tests.Mvc.Controllers.TopControllerTests
{
    [TestFixture]
    public class TopBreweries_Should : TopControllerTestBase
    {
        [Test]
        public void Call_TopServiceGetTopBreweriesMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<TopController>();
            var topService = this.Kernel.GetMock<ITopBeersService>();
            // Act
            sut.TopBreweries();
            // Assert
            topService.Verify(s => s.GetTopBreweries(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void Return_ViewResultWithCorrectModel()
        {
            // Arrange
            var sut = this.Kernel.Get<TopController>();
            var expectedTopBreweries = new List<IBreweryRank>() { new Mock<IBreweryRank>().Object };
            var topService = this.Kernel.GetMock<ITopBeersService>();
            topService.Setup(s => s.GetTopBreweries(It.IsAny<int>()))
                      .Returns(expectedTopBreweries);

            // Act
            var result = sut.TopBreweries();

            // Assert
            var actual = result.Model as IEnumerable<IBreweryRank>;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedTopBreweries, actual);
        }
    }
}
