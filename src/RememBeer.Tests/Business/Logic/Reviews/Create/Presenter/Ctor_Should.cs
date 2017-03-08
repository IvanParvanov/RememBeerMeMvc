using System;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Reviews.Create;
using RememBeer.Business.Logic.Reviews.Create.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Business.Logic.Reviews.Create.Presenter
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenImageUploadIsNull()
        {
            var reviewService = new Mock<IBeerReviewService>();
            var view = new Mock<ICreateReviewView>();

            Assert.Throws<ArgumentNullException>(
                                                 () =>
                                                     new CreateReviewPresenter(reviewService.Object, null, view.Object));
        }
    }
}
