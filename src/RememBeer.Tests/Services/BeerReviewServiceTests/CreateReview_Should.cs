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
            var id = this.Fixture.Create<string>();
            var review = new BeerReview();
            var repository = new Mock<IRepository<BeerReview>>();
            repository.Setup(r => r.GetById(id))
                      .Returns(review);

            var reviewService = new BeerReviewService(repository.Object);

            reviewService.CreateReview(review);

            repository.Verify(r => r.Add(review), Times.Once);
        }

        [Test]
        public void Call_RepositorySaveChangesMethodOnceAndReturnItsValue()
        {
            var expected = new Mock<IDataModifiedResult>();
            var review = new BeerReview();
            var repository = new Mock<IRepository<BeerReview>>();
            repository.Setup(r => r.SaveChanges())
                      .Returns(expected.Object);

            var reviewService = new BeerReviewService(repository.Object);

            var actual = reviewService.CreateReview(review);

            repository.Verify(r => r.SaveChanges(), Times.Once);
            Assert.AreSame(expected.Object, actual);
        }
    }
}
