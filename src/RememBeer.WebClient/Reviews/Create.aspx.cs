using System;
using System.IO;

using Microsoft.AspNet.Identity;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Reviews.Create;
using RememBeer.Business.Logic.Reviews.Create.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Reviews
{
    [PresenterBinding(typeof(CreateReviewPresenter))]
    public partial class Create : BaseMvpPage<StatelessViewModel>, ICreateReviewView
    {
        public event EventHandler<IBeerReviewInfoEventArgs> OnCreateReview;

        public string ErrorMessageText
        {
            get
            {
                return this.Notifier.ErrorText;
            }
            set
            {
                this.Notifier.ErrorText = value;
            }
        }

        public bool ErrorMessageVisible
        {
            get
            {
                return this.Notifier.ErrorVisible;
            }
            set
            {
                this.Notifier.ErrorVisible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void InsertButton_OnClick(object sender, EventArgs e)
        {
            var userId = this.User.Identity.GetUserId();
            var review = new BeerReview()
                         {
                             BeerId = int.Parse(this.HiddenBeerId.Value),
                             Place = this.TbPlace.Text,
                             Description = this.TbDescription.Text,
                             Overall = int.Parse(this.BeerRatingSelect5.SelectedValue),
                             Taste = int.Parse(this.BeerRatingSelect6.SelectedValue),
                             Look = int.Parse(this.BeerRatingSelect7.SelectedValue),
                             Smell = int.Parse(this.BeerRatingSelect8.SelectedValue),
                             ApplicationUserId = userId
                         };

            var uploadControl = this.ImageUpload;
            byte[] image = null;
            if (uploadControl.HasFile)
            {
                var extension = Path.GetExtension(uploadControl.PostedFile.FileName).ToLower();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".psd")
                {
                    this.ErrorMessageText = "The uploaded file is not a valid image!";
                    this.ErrorMessageVisible = true;
                    return;
                }

                if (uploadControl.PostedFile.ContentLength >= 4193280)
                {
                    this.ErrorMessageText = "Your image must be less than 4MB!";
                    this.ErrorMessageVisible = true;
                    return;
                }

                image = uploadControl.FileBytes;
            }

            var args = this.EventArgsFactory.CreateBeerReviewInfoEventArgs(review, image);
            this.OnCreateReview?.Invoke(this, args);
        }
    }
}
