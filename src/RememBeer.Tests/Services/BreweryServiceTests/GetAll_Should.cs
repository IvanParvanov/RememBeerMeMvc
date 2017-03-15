using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BreweryServiceTests
{
    [TestFixture]
    public class GetAll_Should : TestClassBase
    {
        [Test]
        public void CallRepositoryGetAllMethodOnce()
        {
            // Arrange
            var repository = new Mock<IRepository<Brewery>>();
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            service.GetAll();

            // Assert
            repository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnResultFromRepositoryGetAllMethod()
        {
            // Arrange
            var expected = new List<Brewery>();
            var repository = new Mock<IRepository<Brewery>>();
            repository.Setup(r => r.GetAll())
                      .Returns(expected);
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var actual = service.GetAll();

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestCase(2, 4, 15)]
        [TestCase(0, 4, 15)]
        [TestCase(4, 4, 20)]
        [TestCase(0, 10, 15)]
        public void ReturnCorrectResult_WhenPaginated(int currentPage,
                                                      int expectedPageSize,
                                                      int expectedTotalCount)
        {
            // Arrange
            var breweryComparer =
                Comparer<IBrewery>.Create(((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal)));

            var breweries = new List<Brewery>();
            for (var i = 0; i < expectedTotalCount; i++)
            {
                breweries.Add(new Brewery()
                              {
                                  Name = this.Fixture.Create<string>()
                              });
            }

            var queryableBreweries = breweries.AsQueryable();
            var repository = new Mock<IRepository<Brewery>>();
            repository.Setup(r => r.All)
                      .Returns(queryableBreweries);
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.GetAll(currentPage, expectedPageSize, (a) => a.Name);

            var actualUsers = result as IBrewery[] ?? result.ToArray();
            var actualCount = actualUsers.Count();

            // Assert
            Assert.GreaterOrEqual(expectedPageSize, actualCount);
            CollectionAssert.IsOrdered(actualUsers, breweryComparer);
        }

        [TestCase(5, "kasdjkl2j3")]
        [TestCase(5, "asi8789798790123897a9sd")]
        [TestCase(5, "asasd654as")]
        [TestCase( 10, "Glarus")]
        public void ReturnCorrectResult_WhenSearching(int foundPerCriteria,
                                                      string search)
        {
            // Arrange
            var breweries = new List<Brewery>();
            breweries.Add(new Brewery() { Name = "", Country = "" });
            breweries.Add(new Brewery() { Name = "", Country = "" });

            for (var i = 0; i < foundPerCriteria; i++)
            {
                breweries.Add(new Brewery()
                              {
                                  Name = this.Fixture.Create<string>() + search + this.Fixture.Create<string>(),
                                  Country = this.Fixture.Create<string>()
                              });
            }

            for (var i = 0; i < foundPerCriteria; i++)
            {
                breweries.Add(new Brewery()
                {
                    Country = this.Fixture.Create<string>() + search + this.Fixture.Create<string>(),
                    Name = this.Fixture.Create<string>()
                });
            }

            var queryableBreweries = breweries.AsQueryable();
            var repository = new Mock<IRepository<Brewery>>();
            repository.Setup(r => r.All)
                      .Returns(queryableBreweries);
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.GetAll(0, int.MaxValue, (a) => a.Name, search);

            // Assert
            var actual = result as IList<IBrewery> ?? result.ToList();
            Assert.AreEqual(foundPerCriteria * 2, actual.Count);
            CollectionAssert.IsSubsetOf(actual, breweries);
        }
    }
}
