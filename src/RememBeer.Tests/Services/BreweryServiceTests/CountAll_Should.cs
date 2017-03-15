using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BreweryServiceTests
{
    [TestFixture]
    public class CountAll_Should : TestClassBase
    {
        [Test]
        public void ReturnCorrectCountOfItems()
        {
            // Arrange
            var expectedBeers = new List<Brewery>() { new Brewery(), new Brewery() };
            var breweryRepository = new Mock<IRepository<Brewery>>();
            breweryRepository.Setup(r => r.All)
                             .Returns(expectedBeers.AsQueryable());
            var beerRepository = new Mock<IRepository<Beer>>();

            var sut = new BreweryService(breweryRepository.Object, beerRepository.Object);

            // Act
            var result = sut.CountAll();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ReturnCorrectCountOfItems_WhenSearchIsSpecified()
        {
            // Arrange
            var searchPattern = Guid.NewGuid().ToString();
            var allBeers = new List<Brewery>()
                                {
                                    new Brewery { Name = "" },
                                    new Brewery { Name = "" },
                                    new Brewery { Name = this.Fixture.Create<string>() + searchPattern },
                                    new Brewery { Name = searchPattern },
                                    new Brewery { Name = this.Fixture.Create<string>() + searchPattern + this.Fixture.Create<string>() },
                                    new Brewery { Name = searchPattern + this.Fixture.Create<string>() },
                                };

            var breweryRepository = new Mock<IRepository<Brewery>>();
            breweryRepository.Setup(r => r.All)
                             .Returns(allBeers.AsQueryable());
            var beerRepository = new Mock<IRepository<Beer>>();

            var sut = new BreweryService(breweryRepository.Object, beerRepository.Object);

            // Act
            var result = sut.CountAll(searchPattern);

            // Assert
            Assert.AreEqual(4, result);
        }
    }
}
