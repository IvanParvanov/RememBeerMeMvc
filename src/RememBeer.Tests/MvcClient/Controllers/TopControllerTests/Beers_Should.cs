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
    public class Beers_Should : TopControllerTestBase
    {
        [TestCase(typeof(ChildActionOnlyAttribute))]
        [TestCase(typeof(OutputCacheAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();

            // Act
            var hasAttributes = AttributeTester.MethodHasAttribute(() => sut.Beers(), attrType);

            // Assert
            Assert.IsTrue(hasAttributes);
        }

        [Test]
        public void Call_TopServiceGetTopBeersMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();
            var topService = this.MockingKernel.GetMock<ITopBeersService>();

            // Act
            sut.Beers();

            // Assert
            topService.Verify(s => s.GetTopBeers(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void Return_ViewResultWithCorrectModel()
        {
            // Arrange
            var sut = this.MockingKernel.Get<TopController>();
            var expectedTopBeers = new List<IBeerRank>() { new Mock<IBeerRank>().Object};
            var topService = this.MockingKernel.GetMock<ITopBeersService>();
            topService.Setup(s => s.GetTopBeers(It.IsAny<int>()))
                      .Returns(expectedTopBeers);

            // Act
            var result = sut.Beers();

            // Assert
            var actual = result.Model as IEnumerable<IBeerRank>;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedTopBeers, actual);
        }
    }
}
