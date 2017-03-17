using System;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Reviews
{
    [TestFixture]
    public class SingleReviewViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = this.Fixture.Create<int>();
            var expectedBeerType = this.Fixture.Create<string>();
            var expectedBrewery = this.Fixture.Create<string>();
            var expectedBeer = this.Fixture.Create<string>();
            var expectedUrl = this.Fixture.Create<string>();
            var expectedDate = DateTime.UtcNow;
            var sut = new SingleReviewViewModel();
            // Act
            sut.BeerBeerTypeType = expectedBeerType;
            sut.BeerBreweryName = expectedBrewery;
            sut.BeerName = expectedBeer;
            sut.CreatedAt = expectedDate;
            sut.ImgUrl = expectedUrl;
            sut.IsEdit = true;
            sut.Id = expectedId;
            // Assert
            Assert.AreEqual(expectedId, sut.Id);
            Assert.AreEqual(true, sut.IsEdit);
            Assert.AreEqual(expectedDate, sut.CreatedAt);
            Assert.AreEqual(expectedBeerType, sut.BeerBeerTypeType);
            Assert.AreEqual(expectedBrewery, sut.BeerBreweryName);
            Assert.AreEqual(expectedBeer, sut.BeerName);
            Assert.AreEqual(expectedUrl, sut.ImgUrl);
        }
    }
}
