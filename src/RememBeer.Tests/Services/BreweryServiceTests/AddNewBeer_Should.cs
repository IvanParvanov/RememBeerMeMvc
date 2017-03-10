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
    public class AddNewBeer_Should : TestClassBase
    {
        [Test]
        public void CallBeerRepositoryAddMethodWithCorrectParametersOnce()
        {
            // Arrange
            var expectedBreweryId = this.Fixture.Create<int>();
            var expectedTypeId = this.Fixture.Create<int>();
            var expectedName = this.Fixture.Create<string>();
            var beerRepository = new Mock<IRepository<Beer>>();
            var breweryRepository = new Mock<IRepository<Brewery>>();
            var service = new BreweryService(breweryRepository.Object, beerRepository.Object);

            // Act
            service.AddNewBeer(expectedBreweryId, expectedTypeId, expectedName);

            // Assert
            beerRepository
                .Verify(x => x.Add(
                                   It.Is<Beer>(a => a.BreweryId == expectedBreweryId
                                                    && a.BeerTypeId == expectedTypeId
                                                    && a.Name == expectedName)),
                        Times.Once);
        }

        [Test]
        public void CallAndReturnValueFromBeerRepositorySaveChangesMethodOnce()
        {
            // Arrange
            var expectedBreweryId = this.Fixture.Create<int>();
            var expectedTypeId = this.Fixture.Create<int>();
            var expectedName = this.Fixture.Create<string>();
            var expectedResult = new Mock<IDataModifiedResult>();
            var beerRepository = new Mock<IRepository<Beer>>();
            beerRepository.Setup(x => x.SaveChanges())
                          .Returns(expectedResult.Object);
            var breweryRepository = new Mock<IRepository<Brewery>>();
            var service = new BreweryService(breweryRepository.Object, beerRepository.Object);

            // Act
            var result = service.AddNewBeer(expectedBreweryId, expectedTypeId, expectedName);

            // Assert
            beerRepository.Verify(r => r.SaveChanges(), Times.Once);
            Assert.AreSame(expectedResult.Object, result);
        }
    }
}
