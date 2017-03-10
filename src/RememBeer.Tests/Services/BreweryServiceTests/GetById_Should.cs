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
    public class GetById_Should : TestClassBase
    {
        [Test]
        public void ReturnResultFromRepository()
        {
            // Arrange
            var id = this.Fixture.Create<string>();
            var expected = new Brewery();
            var repository = new Mock<IRepository<Brewery>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(expected);
            var beerRepo = new Mock<IRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.GetById(id);

            // Assert
            Assert.AreSame(expected, result);
        }
    }
}
