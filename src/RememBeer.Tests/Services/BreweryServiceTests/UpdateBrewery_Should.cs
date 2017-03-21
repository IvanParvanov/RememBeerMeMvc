using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories;
using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BreweryServiceTests
{
    [TestFixture]
    public class UpdateBrewery_Should : TestClassBase
    {
        [Test]
        public void CallRepositoryGetByIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var id = this.Fixture.Create<int>();
            var name = this.Fixture.Create<string>();
            var descr = this.Fixture.Create<string>();
            var country = this.Fixture.Create<string>();
            var expected = new Brewery();
            var repository = new Mock<IEfRepository<Brewery>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(expected);
            var beerRepo = new Mock<IEfRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.UpdateBrewery(id, name, country, descr);

            // Assert
            repository.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void CallRepositoryUpdateMethodOnceWithCorrectParams()
        {
            // Arrange
            var id = this.Fixture.Create<int>();
            var name = this.Fixture.Create<string>();
            var descr = this.Fixture.Create<string>();
            var country = this.Fixture.Create<string>();
            var brewery = new Brewery();

            var repository = new Mock<IEfRepository<Brewery>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(brewery);
            var beerRepo = new Mock<IEfRepository<Beer>>();

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.UpdateBrewery(id, name, country, descr);

            // Assert
            repository.Verify(r => r.Update(brewery), Times.Once);
            Assert.AreSame(name, brewery.Name);
            Assert.AreSame(descr, brewery.Description);
            Assert.AreSame(country, brewery.Country);
        }

        [Test]
        public void ReturnResultFromSaveChanges()
        {
            // Arrange
            var expectedResult = new Mock<IDataModifiedResult>();
            var id = this.Fixture.Create<int>();
            var name = this.Fixture.Create<string>();
            var descr = this.Fixture.Create<string>();
            var country = this.Fixture.Create<string>();
            var brewery = new Brewery();
            var beerRepo = new Mock<IEfRepository<Beer>>();

            var repository = new Mock<IEfRepository<Brewery>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(brewery);
            repository.Setup(r => r.SaveChanges())
                      .Returns(expectedResult.Object);

            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.UpdateBrewery(id, name, country, descr);

            // Assert
            Assert.AreSame(expectedResult.Object, result);
        }
    }
}
