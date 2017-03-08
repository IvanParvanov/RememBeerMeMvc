using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Models.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Reviews.My.EventArgs
{
    [TestFixture]
    public class BeerReviewEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetUpPropertiesCorrectly()
        {
            var review = new Mock<IBeerReview>();
            var image = new byte[1];

            var args = new BeerReviewInfoEventArgs(review.Object, image);

            Assert.AreSame(review.Object, args.BeerReview);
            Assert.AreSame(image, args.Image);
        }

        [Test]
        public void Ctor_ShouldSetUpPropertiesCorrectly_WhenCalledWithOneArg()
        {
            var review = new Mock<IBeerReview>();

            var args = new BeerReviewInfoEventArgs(review.Object);

            Assert.AreSame(review.Object, args.BeerReview);
        }
    }
}
