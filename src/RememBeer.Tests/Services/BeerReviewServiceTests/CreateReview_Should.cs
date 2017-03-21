using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories;
using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BeerReviewServiceTests
{
    [TestFixture]
    public class CreateReview_Should : TestClassBase
    {
        [Test]
        public void Call_RepositoryAddMethodOnceWithCorrectParams()
        {
            // Arrange
            var id = this.Fixture.Create<string>();
            var review = new BeerReview();
            var repository = new Mock<IEfRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            // Act
            reviewService.CreateReview(review);

            // Assert
            repository.Verify(r => r.Add(review), Times.Once);
        }

        [Test]
        public void Call_RepositorySaveChangesMethodOnceAndReturnItsValue()
        {
            // Arrange
            var expected = new Mock<IDataModifiedResult>();
            var review = new BeerReview();
            var repository = new Mock<IEfRepository<BeerReview>>();
            repository.Setup(r => r.SaveChanges())
                      .Returns(expected.Object);

            var reviewService = new BeerReviewService(repository.Object);

            // Act
            var actual = reviewService.CreateReview(review);

            // Assert
            repository.Verify(r => r.SaveChanges(), Times.Once);
            Assert.AreSame(expected.Object, actual);
        }
    }
}
