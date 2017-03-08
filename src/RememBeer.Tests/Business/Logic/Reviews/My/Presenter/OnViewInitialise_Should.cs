using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Reviews.My.Presenter
{
    [TestFixture]
    public class OnViewInitialise_Should : TestClassBase
    {
        [Test]
        public void Call_GetReviewsForUserWithCorrectParamsOnce()
        {
            var args = MockedEventArgsGenerator.GetUserReviewsEventArgs();
            var expectedReviews = new List<BeerReview>();
            var viewModel = new ReviewsViewModel()
            {
                Reviews = expectedReviews
            };
            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);
            view.SetupSet(v => v.SuccessMessageVisible = false);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(args.UserId, args.StartRowIndex, args.PageSize))
                         .Returns(expectedReviews);


            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
            {
                HttpContext = new MockedHttpContextBase(httpResponse)
            };

            view.Raise(v => v.Initialized += null, view.Object, args);

           reviewService.Verify(r => r.GetReviewsForUser(args.UserId, args.StartRowIndex, args.PageSize), Times.Once());
        }

        [Test]
        public void Set_ViewModelTotalCountPropertyToReturnValueFromCountUserReviews()
        {
            var expectedTotalCount = this.Fixture.Create<int>();
            var args = MockedEventArgsGenerator.GetUserReviewsEventArgs();
            var expectedReviews = new List<BeerReview>();
            var viewModel = new ReviewsViewModel()
            {
                Reviews = expectedReviews
            };
            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);
            view.SetupSet(v => v.SuccessMessageVisible = false);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                         .Returns(expectedReviews);
            reviewService.Setup(s => s.CountUserReviews(It.IsAny<string>()))
                         .Returns(expectedTotalCount);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
            {
                HttpContext = new MockedHttpContextBase(httpResponse)
            };

            view.Raise(v => v.Initialized += null, view.Object, args);

            Assert.AreEqual(expectedTotalCount, viewModel.TotalCount);
        }

        [Test]
        public void Set_ModelReviewsCorrectly()
        {
            var args = MockedEventArgsGenerator.GetUserReviewsEventArgs();
            var expectedReviews = new List<BeerReview>();
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };
            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                         .Returns(expectedReviews);

            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.Initialized += null, view.Object, args);

            Assert.AreSame(view.Object.Model.Reviews, expectedReviews);
        }

        [Test]
        public void Hide_SuccessMessage()
        {
            var args = MockedEventArgsGenerator.GetUserReviewsEventArgs();
            var expectedReviews = new List<BeerReview>();
            var viewModel = new ReviewsViewModel()
                            {
                                Reviews = expectedReviews
                            };
            var view = new Mock<IMyReviewsView>();
            view.SetupGet(v => v.Model).Returns(viewModel);
            view.SetupSet(v => v.SuccessMessageVisible = false);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(s => s.GetReviewsForUser(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                         .Returns(expectedReviews);
            var httpResponse = new MockedHttpResponse();
            var presenter = new MyReviewsPresenter(reviewService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.Initialized += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageVisible = false, Times.Once());
        }
    }
}
