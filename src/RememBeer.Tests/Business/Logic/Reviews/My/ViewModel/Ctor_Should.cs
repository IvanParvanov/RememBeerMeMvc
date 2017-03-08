using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Business.Logic.Reviews.My.ViewModel
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void SetReviewsToEmptyHashset()
        {
            var viewModel = new ReviewsViewModel();

            Assert.IsNotNull(viewModel.Reviews);
            Assert.IsInstanceOf<HashSet<IBeerReview>>(viewModel.Reviews);
            CollectionAssert.IsEmpty(viewModel.Reviews);
        }
    }
}
