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
    public class DeleteReview_Should : TestClassBase
    {
        [Test]
        public void Call_RepositoryGetByIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var id = this.Fixture.Create<string>();
            var review = new BeerReview();
            var repository = new Mock<IEfRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            // Act
            reviewService.DeleteReview(id);

            // Assert
            repository.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void SetIsDeletedPropertyToTrue()
        {
            // Arrange
            var id = this.Fixture.Create<string>();
            var review = new BeerReview
                         {
                             IsDeleted = false
                         };

            var repository = new Mock<IEfRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            // Act
            reviewService.DeleteReview(id);

            // Assert
            Assert.IsTrue(review.IsDeleted);
        }

        [Test]
        public void Call_RepositorySaveChangesMethodOnceAndReturnItsValue()
        {
            // Arrange
            var expected = new Mock<IDataModifiedResult>();
            var id = this.Fixture.Create<string>();
            var review = new BeerReview
                         {
                             IsDeleted = false
                         };

            var repository = new Mock<IEfRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);
            repository.Setup(r => r.SaveChanges())
                      .Returns(expected.Object);

            var reviewService = new BeerReviewService(repository.Object);

            // Act
            var result = reviewService.DeleteReview(id);

            // Assert
            Assert.AreSame(expected.Object, result);
            repository.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}
