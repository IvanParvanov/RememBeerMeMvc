using System.Collections.Generic;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.TopControllerTests
{
    [TestFixture]
    public class TopBeers_Should : TopControllerTestBase
    {
        [Test]
        public void Call_TopServiceGetTopBeersMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.Kernel.Get<TopController>();
            var topService = this.Kernel.GetMock<ITopBeersService>();
            // Act
            sut.TopBeers();
            // Assert
            topService.Verify(s => s.GetTopBeers(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void Return_ViewResultWithCorrectModel()
        {
            // Arrange
            var sut = this.Kernel.Get<TopController>();
            var expectedTopBeers = new List<IBeerRank>() { new Mock<IBeerRank>().Object};
            var topService = this.Kernel.GetMock<ITopBeersService>();
            topService.Setup(s => s.GetTopBeers(It.IsAny<int>()))
                      .Returns(expectedTopBeers);

            // Act
            var result = sut.TopBeers();

            // Assert
            var actual = result.Model as IEnumerable<IBeerRank>;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedTopBeers, actual);
        }
    }
}
