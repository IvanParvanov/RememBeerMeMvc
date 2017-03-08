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
            var id = this.Fixture.Create<string>();
            var review = new BeerReview();
            var repository = new Mock<IRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            reviewService.DeleteReview(id);

            repository.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void SetIsDeletedPropertyToTrue()
        {
            var id = this.Fixture.Create<string>();
            var review = new BeerReview
                         {
                             IsDeleted = false
                         };

            var repository = new Mock<IRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            reviewService.DeleteReview(id);

            Assert.IsTrue(review.IsDeleted);
        }

        [Test]
        public void Call_RepositorySaveChangesMethodOnceAndReturnItsValue()
        {
            var expected = new Mock<IDataModifiedResult>();
            var id = this.Fixture.Create<string>();
            var review = new BeerReview
                         {
                             IsDeleted = false
                         };

            var repository = new Mock<IRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);
            repository.Setup(r => r.SaveChanges())
                      .Returns(expected.Object);

            var reviewService = new BeerReviewService(repository.Object);

            var result = reviewService.DeleteReview(id);

            Assert.AreSame(expected.Object, result);
            repository.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}
