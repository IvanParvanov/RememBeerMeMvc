using System;

using RememBeer.Business.Logic.Reviews.Common.Presenters;
using RememBeer.Business.Logic.Reviews.Create.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Common.Constants;
using RememBeer.Common.Services.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Reviews.Create
{
    public class CreateReviewPresenter : BeerReviewPresenter<ICreateReviewView>
    {
        private readonly IImageUploadService imgUploadService;

        public CreateReviewPresenter(IBeerReviewService reviewService,
                                     IImageUploadService imgUploadService,
                                     ICreateReviewView view)
            : base(reviewService, view)
        {
            if (imgUploadService == null)
            {
                throw new ArgumentNullException(nameof(imgUploadService));
            }

            this.imgUploadService = imgUploadService;
            this.View.OnCreateReview += this.OnViewCreateReview;
        }

        private void OnViewCreateReview(object sender, IBeerReviewInfoEventArgs e)
        {
            var review = e.BeerReview;
            var image = e.Image;
            if (image != null)
            {
                var url = this.imgUploadService.UploadImage(image, Constants.DefaultThumbnailSizePx, Constants.DefaultThumbnailSizePx);
                review.ImgUrl = url ?? review.ImgUrl;
            }

            var result = this.ReviewService.CreateReview(review);
            if (result.Successful)
            {
                //this.View.SuccessMessageText = "Review has been successfully created!";
                //this.View.SuccessMessageVisible = true;

                this.Response.Redirect("/Reviews/My");
            }
            else
            {
                this.View.ErrorMessageText = string.Join(", ", result.Errors);
            }
        }
    }
}
