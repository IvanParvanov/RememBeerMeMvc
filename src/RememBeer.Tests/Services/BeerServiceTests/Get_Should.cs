using Moq;

using NUnit.Framework;

using RememBeer.Data.Repositories.Base;
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
        public void Call_RepositoryGetByIdMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var repository = new Mock<IRepository<Beer>>();
            var sut = new BeerService(repository.Object);

            // Act
            sut.Get(expectedId);

            // Assert
            repository.Verify(r => r.GetById(expectedId), Times.Once);
        }

        public void ReturnResultFrom_RepositoryGetByIdMethod()
        {
            // Arrange
            var expectedResult = new Beer();
            var repository = new Mock<IRepository<Beer>>();
            repository.Setup(r => r.GetById(It.IsAny<int>()))
                      .Returns(expectedResult);
            var sut = new BeerService(repository.Object);

            // Act
            var result = sut.Get(It.IsAny<int>());

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
