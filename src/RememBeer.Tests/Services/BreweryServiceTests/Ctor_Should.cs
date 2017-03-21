using System;

using Moq;

using NUnit.Framework;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;

namespace RememBeer.Tests.Services.BreweryServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBreweryRepositoryIsNull()
        {
            // Arrange
            var beerRepo = new Mock<IEfRepository<Beer>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BreweryService(null, beerRepo.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBeerRepositoryIsNull()
        {
            // Arrange
            var breweryRepo = new Mock<IEfRepository<Brewery>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BreweryService(breweryRepo.Object, null));
        }
    }
}
