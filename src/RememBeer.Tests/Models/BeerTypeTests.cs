using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Models;

namespace RememBeer.Tests.Models
{
    [TestFixture]
    public class BeerTypeTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = 1;
            var expectedType = "sdadasdasdas";
            var beers = new List<Beer>();

            // Act
            var beer = new BeerType()
            {
                           Id = expectedId,
                           Type = expectedType,
                           Beers = beers
                       };

            // Assert
            Assert.AreEqual(expectedId, beer.Id);
            Assert.AreEqual(expectedType, beer.Type);
            Assert.AreSame(beers, beer.Beers);
        }

        [Test]
        public void Ctor_ShouldInitializePropertiesCorrectly()
        {
            // Act
            var beer = new BeerType();

            // Assert
            Assert.IsNotNull(beer.Beers);
            Assert.IsInstanceOf<HashSet<Beer>>(beer.Beers);
        }
    }
}
