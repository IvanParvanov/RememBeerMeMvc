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
            var expectedId = 1;
            var expectedType = "sdadasdasdas";
            var beers = new List<Beer>();
            var beer = new BeerType()
                       {
                           Id = expectedId,
                           Type = expectedType,
                           Beers = beers
                       };

            Assert.AreEqual(expectedId, beer.Id);
            Assert.AreEqual(expectedType, beer.Type);
            Assert.AreSame(beers, beer.Beers);
        }

        [Test]
        public void Ctor_ShouldInitializePropertiesCorrectly()
        {
            var beer = new BeerType();

            Assert.IsNotNull(beer.Beers);
            Assert.IsInstanceOf<HashSet<Beer>>(beer.Beers);
        }
    }
}
