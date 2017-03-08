using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Business.Logic.Reviews.Common.Presenters;
using RememBeer.Business.Logic.Reviews.Default.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Reviews.Default
{
    public class DefaultPresenter : BeerReviewPresenter<IReviewDetailsView>
    {
        public DefaultPresenter(IBeerReviewService reviewService, IReviewDetailsView view)
            : base(reviewService, view)
        {
            this.View.OnInitialise += this.OnViewInitialise;
        }

        private void OnViewInitialise(object sender, IIdentifiableEventArgs<int> e)
        {
            var id = e.Id;
            var review = this.ReviewService.GetById(id);
            if (review != null)
            {
                this.View.Model.Review = review;
                this.View.NotFoundVisible = false;
            }
            else
            {
                this.View.NotFoundVisible = true;
            }
        }
    }
}
