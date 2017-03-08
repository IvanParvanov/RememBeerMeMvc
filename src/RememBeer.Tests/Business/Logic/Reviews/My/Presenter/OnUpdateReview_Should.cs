using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Reviews.My.Presenter
{
    [TestFixture]
    public class OnUpdateReview_Should : TestClassBase
    {
        [Test]
        public void CallUpdateReviewMethodOnce()
        {
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();
            var view = new Mock<IMyReviewsView>();
            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(false);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.UpdateReview(args.BeerReview))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewUpdate += null, view.Object, args);

            reviewService.Verify(s => s.UpdateReview(args.BeerReview), Times.Once);
        }

        [Test]
        public void SetViewPropertiesCorrectly()
        {
            const string ExpectedMessage = "Review successfully updated!";
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();

            var view = new Mock<IMyReviewsView>();

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(true);

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.UpdateReview(args.BeerReview))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewUpdate += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageText = ExpectedMessage, Times.Once);
            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
        }

        [Test]
        public void CatchUpdateExceptionAndSetViewProperties()
        {
            var expectedMessage = this.Fixture.Create<string>();

            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();

            var view = new Mock<IMyReviewsView>();

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(false);
            result.Setup(r => r.Errors).Returns(new[] { expectedMessage });

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.UpdateReview(args.BeerReview))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewUpdate += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageText = expectedMessage, Times.Once);
            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
        }
    }
}
