using System;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Account.Register.Contracts;
using RememBeer.Business.Logic.Reviews.Common.Presenters;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Business.Logic.Reviews.Common.Presenters
{
    [TestFixture]
    public class BeerReviewPresenterTests
    {
        [Test]
        public void Ctor_ShouldThrowArgumentNullException_WhenServiceIsNull()
        {
            var view = new Mock<IRegisterView>();
            Assert.Throws<ArgumentNullException>(
                                                 () =>
                                                     new BeerReviewPresenter<IRegisterView>(null,
                                                                                            view.Object));
        }

        [Test]
        public void Ctor_SetReviewService()
        {
            var view = new Mock<IRegisterView>();
            var service = new Mock<IBeerReviewService>();
            var presenter = new MockedBeerReviewPresenter(service.Object, view.Object);

            Assert.AreSame(service.Object, presenter.ActualReviewService);
        }
    }

    public class MockedBeerReviewPresenter : BeerReviewPresenter<IRegisterView>
    {
        public MockedBeerReviewPresenter(IBeerReviewService reviewService, IRegisterView view)
            : base(reviewService, view)
        {
        }

        public IBeerReviewService ActualReviewService => base.ReviewService;
    }
}
