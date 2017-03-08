using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.Default;
using RememBeer.Business.Logic.Reviews.Default.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Reviews.Default.Presenter
{
    [TestFixture]
    public class OnViewinitialise_Should : TestClassBase
    {
        [Test]
        public void CallReviewServiceGetByIdMethodOnceWithCorrectParams()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<int>();
            var reviewService = new Mock<IBeerReviewService>();
            var view = new Mock<IReviewDetailsView>();
            var presenter = new DefaultPresenter(reviewService.Object, view.Object);

            view.Raise(v => v.OnInitialise += null, view.Object, args);

            reviewService.Verify(s => s.GetById(args.Id), Times.Once);
        }

        [Test]
        public void SetViewProperties_WhenReviewIsFound()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<int>();
            var expectedBeerReview = new Mock<IBeerReview>();
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(args.Id))
                         .Returns(expectedBeerReview.Object);

            var viewModel = new MockedBeerReviewViewModel();
            var view = new Mock<IReviewDetailsView>();
            view.Setup(v => v.Model).Returns(viewModel);

            var presenter = new DefaultPresenter(reviewService.Object, view.Object);

            view.Raise(v => v.OnInitialise += null, view.Object, args);

            view.VerifySet(v => v.NotFoundVisible = false, Times.Once);
            Assert.AreSame(expectedBeerReview.Object, view.Object.Model.Review);
        }

        [Test]
        public void SetViewProperties_WhenReviewIsNotFound()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<int>();
            var expectedId = this.Fixture.Create<int>();

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetById(expectedId))
                         .Returns((IBeerReview)null);

            var view = new Mock<IReviewDetailsView>();
            var presenter = new DefaultPresenter(reviewService.Object, view.Object);

            view.Raise(v => v.OnInitialise += null, view.Object, args);

            view.VerifySet(v => v.NotFoundVisible = true, Times.Once);
        }
    }
}
