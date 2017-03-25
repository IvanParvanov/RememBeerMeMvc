using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.TopControllerTests
{
    [TestFixture]
    public class Breweries_Should : TopControllerTestBase
    {
        [TestCase(typeof(ChildActionOnlyAttribute))]
        [TestCase(typeof(OutputCacheAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();

            // Act
            var hasAttributes = AttributeTester.MethodHasAttribute(() => sut.Breweries(), attrType);

            // Assert
            Assert.IsTrue(hasAttributes);
        }

        [Test]
        public void Call_TopServiceGetTopBreweriesMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();
            var topService = this.MockingKernel.GetMock<ITopBeersService>();

            // Act
            sut.Breweries();

            // Assert
            topService.Verify(s => s.GetTopBreweries(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void Return_ViewResultWithCorrectModel()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();
            var expectedTopBreweries = new List<IBreweryRank>() { new Mock<IBreweryRank>().Object };
            var topService = this.MockingKernel.GetMock<ITopBeersService>();
            topService.Setup(s => s.GetTopBreweries(It.IsAny<int>()))
                      .Returns(expectedTopBreweries);

            // Act
            var result = sut.Breweries();

            // Assert
            var actual = result.Model as IEnumerable<IBreweryRank>;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedTopBreweries, actual);
        }
    }
}
