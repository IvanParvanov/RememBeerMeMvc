using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Models;

namespace RememBeer.Tests.Models
{
    [TestFixture]
    public class BreweryTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = 1;
            var expectedCountry = "BG";
            var expectedDescription = "BGBrewery";
            var expectedName = "Glarus";
            var beers = new HashSet<Beer>();

            // Act
            var brewery = new Brewery()
            {
                              Id = expectedId,
                              Country = expectedCountry,
                              Description = expectedDescription,
                              Name = expectedName,
                              Beers = beers
                          };

            // Assert
            Assert.AreEqual(expectedId, brewery.Id);
            Assert.AreEqual(expectedCountry, brewery.Country);
            Assert.AreEqual(expectedDescription, brewery.Description);
            Assert.AreEqual(expectedName, brewery.Name);
            Assert.AreSame(beers, brewery.Beers);
        }
    }
}
