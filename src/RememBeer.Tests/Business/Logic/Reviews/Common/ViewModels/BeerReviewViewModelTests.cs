using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Reviews.Common.ViewModels;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Business.Logic.Reviews.Common.ViewModels
{
    [TestFixture]
    public class BeerReviewViewModelTests
    {
        [Test]
        public void Setters_ShouldSetCorrectly()
        {
            var review = new Mock<IBeerReview>();
            var sut = new BeerReviewViewModel();
            sut.Review = review.Object;

            Assert.AreSame(review.Object, sut.Review);
        }
    }
}
