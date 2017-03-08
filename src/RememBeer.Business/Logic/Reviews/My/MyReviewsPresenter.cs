using System.Linq;

using RememBeer.Business.Logic.Reviews.Common.Presenters;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Reviews.My
{
    public class MyReviewsPresenter : BeerReviewPresenter<IMyReviewsView>
    {
        public MyReviewsPresenter(IBeerReviewService reviewService, IMyReviewsView view)
            : base(reviewService, view)
        {
            this.View.Initialized += this.OnViewInitialise;
            this.View.ReviewUpdate += this.OnUpdateReview;
            this.View.ReviewDelete += this.OnDeleteReview;
        }

        private void OnDeleteReview(object sender, IBeerReviewInfoEventArgs e)
        {
            var id = e.BeerReview.Id;
            var result = this.ReviewService.DeleteReview(id);
            if (result.Successful)
            {
                this.View.Model.Reviews = this.View.Model.Reviews.Where(r => !r.IsDeleted).ToList();
                this.View.SuccessMessageText = "Review deleted!";
                this.View.SuccessMessageVisible = true;
            }
            else
            {
                this.View.SuccessMessageText = string.Join(", ", result.Errors);
                this.View.SuccessMessageVisible = true;
            }
        }

        private void OnUpdateReview(object sender, IBeerReviewInfoEventArgs e)
        {
            var review = e.BeerReview;

            var result = this.ReviewService.UpdateReview(review);
            if (result.Successful)
            {
                this.View.SuccessMessageText = "Review successfully updated!";
                this.View.SuccessMessageVisible = true;
            }
            else
            {
                this.View.SuccessMessageText = string.Join(", ", result.Errors);
                this.View.SuccessMessageVisible = true;
            }
        }

        private void OnViewInitialise(object sender, IUserReviewsEventArgs e)
        {
            this.View.SuccessMessageVisible = false;

            var beerReviews = this.ReviewService.GetReviewsForUser(e.UserId, e.StartRowIndex, e.PageSize);

            this.View.Model.Reviews = beerReviews;
            this.View.Model.TotalCount = this.ReviewService.CountUserReviews(e.UserId);
        }
    }
}
