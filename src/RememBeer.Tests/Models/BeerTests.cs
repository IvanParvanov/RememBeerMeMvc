using System.Collections.Generic;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Models
{
    [TestFixture]
    public class BeerTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            var expectedId = this.Fixture.Create<int>();
            var expectedBreweryId = this.Fixture.Create<int>();
            var expectedBeerTypeId = this.Fixture.Create<int>();
            var expectedName = this.Fixture.Create<string>();
            var beer = new Beer()
                       {
                           Id = expectedId,
                           Brewery = null,
                           BeerType = null,
                           BreweryId = expectedBreweryId,
                           BeerTypeId = expectedBeerTypeId,
                           Name = expectedName
                       };

            Assert.AreEqual(expectedId, beer.Id);
            Assert.AreEqual(expectedBeerTypeId, beer.BeerTypeId);
            Assert.AreEqual(expectedBreweryId, beer.BreweryId);
            Assert.AreEqual(null, beer.Brewery);
            Assert.AreEqual(null, beer.BeerType);
            Assert.AreSame(expectedName, beer.Name);
            Assert.IsInstanceOf<HashSet<BeerReview>>(beer.Reviews);
        }
    }
}
