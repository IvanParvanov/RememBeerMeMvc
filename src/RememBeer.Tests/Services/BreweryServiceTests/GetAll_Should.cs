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
            var repository = new Mock<IRepository<Brewery>>();
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            service.GetAll();

            repository.Verify(r => r.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnResultFromRepositoryGetAllMethod()
        {

            var expected = new List<Brewery>();
            var repository = new Mock<IRepository<Brewery>>();
            repository.Setup(r => r.GetAll())
                      .Returns(expected);
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            var actual = service.GetAll();
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
            var result = service.GetAll(currentPage, expectedPageSize, (a) => a.Name);

            var actualUsers = result as IBrewery[] ?? result.ToArray();
            var actualCount = actualUsers.Count();

            Assert.GreaterOrEqual(expectedPageSize, actualCount);
            CollectionAssert.IsOrdered(actualUsers, breweryComparer);
        }
    }
}
