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
    public class DeleteBeer_Should : TestClassBase
    {
        [Test]
        public void Call_BeerRepositoryGetByIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var id = this.Fixture.Create<int>();
            var expected = new Beer()
                           {
                               IsDeleted = false
                           };
            var repository = new Mock<IRepository<Brewery>>();
            var beerRepo = new Mock<IRepository<Beer>>();
            beerRepo.Setup(r => r.GetById(id))
                                  .Returns(expected);
            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            service.DeleteBeer(id);

            // Assert
            beerRepo.Verify(r =>r.GetById(id), Times.Once);
        }

        [Test]
        public void Set_FoundBeerIsDeletedPropertyToTrue()
        {
            // Arrange
            var id = this.Fixture.Create<int>();
            var expected = new Beer()
            {
                IsDeleted = false
            };
            var repository = new Mock<IRepository<Brewery>>();
            var beerRepo = new Mock<IRepository<Beer>>();
            beerRepo.Setup(r => r.GetById(id))
                                  .Returns(expected);
            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            service.DeleteBeer(id);

            // Assert
            Assert.IsTrue(expected.IsDeleted);
        }

        [Test]
        public void ReturnResultFromRepositorySaveChangesMethod()
        {
            // Arrange
            var id = this.Fixture.Create<int>();
            var beer = new Beer();
            var expected = new Mock<IDataModifiedResult>();
            var repository = new Mock<IRepository<Brewery>>();
            var beerRepo = new Mock<IRepository<Beer>>();
            beerRepo.Setup(r => r.GetById(id))
                                  .Returns(beer);
            beerRepo.Setup(r => r.SaveChanges())
                    .Returns(expected.Object);
            var service = new BreweryService(repository.Object, beerRepo.Object);

            // Act
            var result = service.DeleteBeer(id);

            // Assert
            Assert.AreSame(expected.Object, result);
        }
    }
}
