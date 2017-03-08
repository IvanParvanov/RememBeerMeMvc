using System;
using System.Collections.Generic;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.UserControls
{
    [PresenterBinding(typeof(MyReviewsPresenter))]
    public partial class ReviewsEditView : BaseMvpUserControl<ReviewsViewModel>, IMyReviewsView
    {
        public event EventHandler<IUserReviewsEventArgs> Initialized;

        public event EventHandler<IBeerReviewInfoEventArgs> ReviewUpdate;

        public event EventHandler<IBeerReviewInfoEventArgs> ReviewDelete;

        public string SuccessMessageText
        {
            get
            {
                return this.Notifier.SuccessText;
            }
            set
            {
                this.Notifier.SuccessText = value;
            }
        }

        public bool SuccessMessageVisible
        {
            get
            {
                return this.Notifier.SuccessVisible;
            }
            set
            {
                this.Notifier.SuccessVisible = value;
            }
        }

        public string UserId { get; set; }

        public IEnumerable<IBeerReview> Select(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            var args = this.EventArgsFactory.CreateUserReviewsEventArgs(startRowIndex, maximumRows, this.UserId);
            this.Initialized?.Invoke(this, args);

            totalRowCount = this.Model.TotalCount;
            return this.Model.Reviews;
        }

        public void UpdateReview(BeerReview newReview)
        {
            var args = this.EventArgsFactory.CreateBeerReviewInfoEventArgs(newReview);
            this.ReviewUpdate?.Invoke(this, args);
        }

        public void DeleteReview(BeerReview review)
        {
            var args = this.EventArgsFactory.CreateBeerReviewInfoEventArgs(review);
            this.ReviewDelete?.Invoke(this, args);
        }
    }
}
