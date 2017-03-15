using System.Data.Entity;

using Moq;

using NUnit.Framework;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Models;
using RememBeer.Services;

namespace RememBeer.Tests.Services.BeerServiceTests
{
    [TestFixture]
    public class Get_Should
    {
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(541)]
        public void Call_DbSetFindMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var dbSet = new Mock<IDbSet<Beer>>();
            var repository = new Mock<IBeersDb>();
            repository.Setup(r => r.Beers)
                .Returns(dbSet.Object);
            var sut = new BeerService(repository.Object);

            // Act
            sut.Get(expectedId);

            // Assert
            dbSet.Verify(d => d.Find(expectedId), Times.Once);
        }

        [Test]
        public void ReturnResultFrom_DbSetFindMethodMethod()
        {
            // Arrange
            var expectedResult = new Beer();
            var repository = new Mock<IBeersDb>();
            repository.Setup(r => r.Beers.Find(It.IsAny<int>()))
                      .Returns(expectedResult);
            var sut = new BeerService(repository.Object);

            // Act
            var result = sut.Get(It.IsAny<int>());

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
