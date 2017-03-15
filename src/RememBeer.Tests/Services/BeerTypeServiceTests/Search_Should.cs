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

namespace RememBeer.Tests.Services.BeerTypeServiceTests
{
    [TestFixture]
    public class Search_Should : TestClassBase
    {
        [TestCase("asdkjasd231231", 0)]
        [TestCase("asdkjasdasdasdasdasasdasdasasd231231", 2)]
        [TestCase("asdkjasd2asda3asdasdasdasdasasdasdasd23121231231", 17)]
        public void ReturnCorrectResult(string searchPattern, int expectedCount)
        {
            // Arrange
            var allTypes = new List<BeerType>();
            for (var i = 0; i < expectedCount; i++)
            {
                var beerTypeName = this.Fixture.Create<string>() + searchPattern + this.Fixture.Create<string>();
                allTypes.Add(new BeerType() {Type = beerTypeName});
            }

            for ( var i = 0; i < expectedCount / 2; i++ )
            {
                var beerTypeName = this.Fixture.Create<string>();
                allTypes.Add(new BeerType() { Type = beerTypeName });
            }

            var repository = new Mock<IRepository<BeerType>>();
            repository.Setup(r => r.All)
                      .Returns(allTypes.AsQueryable);
            var sut = new BeerTypesService(repository.Object);

            // Act
            var result = sut.Search(searchPattern);

            // Assert
            var actual = result as IList<IBeerType> ?? result.ToList();
            Assert.AreEqual(expectedCount, actual.Count);
            foreach (var beerType in actual)
            {
                StringAssert.Contains(searchPattern, beerType.Type);
            }
        }
    }
}
