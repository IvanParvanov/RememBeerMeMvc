using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Reviews.My.Presenter
{
    [TestFixture]
    public class OnDeleteReview_Should : TestClassBase
    {
        [Test]
        public void CallServiceDeleteReviewMethodOnce_WithCorrectParameter()
        {
            var reviewId = this.Fixture.Create<int>();

            var review = new Mock<IBeerReview>();
            review.Setup(r => r.Id)
                  .Returns(reviewId);
            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);

            var expectedReviews = new List<IBeerReview>()
                                  {
                                      review.Object
                                  };
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };

            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(false);

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.DeleteReview(reviewId))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewDelete += null, view.Object, args.Object);

            reviewService.Verify(s => s.DeleteReview(reviewId), Times.Once);
        }

        [Test]
        public void FilterViewModelsReviewsByDeletion()
        {
            var reviewId = this.Fixture.Create<int>();

            var review = new Mock<IBeerReview>();
            review.Setup(r => r.Id)
                  .Returns(reviewId);
            review.Setup(r => r.IsDeleted)
                  .Returns(true);

            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);

            var expectedReviews = new List<IBeerReview>()
                                  {
                                      review.Object
                                  };
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };

            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(true);

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.DeleteReview(reviewId))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewDelete += null, view.Object, args.Object);

            CollectionAssert.IsEmpty(viewModel.Reviews);
        }

        [Test]
        public void SetViewMessagesCorrectly()
        {
            const string ExpectedMessage = "Review deleted!";

            var reviewId = this.Fixture.Create<int>();

            var review = new Mock<IBeerReview>();
            review.Setup(r => r.Id)
                  .Returns(reviewId);

            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);

            var expectedReviews = new List<IBeerReview>()
                                  {
                                      review.Object
                                  };
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };

            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(true);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.DeleteReview(reviewId))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewDelete += null, view.Object, args.Object);

            view.VerifySet(v => v.SuccessMessageText = ExpectedMessage, Times.Once);
            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
        }

        [Test]
        public void SetViewProperties_WhenResultIsNotSuccessfull()
        {
            var expectedMessage = this.Fixture.Create<string>();

            var reviewId = this.Fixture.Create<int>();

            var review = new Mock<IBeerReview>();
            review.Setup(r => r.Id)
                  .Returns(reviewId);

            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);

            var expectedReviews = new List<IBeerReview>()
                                  {
                                      review.Object
                                  };
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };

            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(false);
            result.Setup(r => r.Errors).Returns(new[] { expectedMessage });

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.DeleteReview(reviewId))
                         .Returns(result.Object);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ReviewDelete += null, view.Object, args.Object);

            view.VerifySet(v => v.SuccessMessageText = expectedMessage, Times.Once);
            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
        }
    }
}
