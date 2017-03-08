using System;

using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Reviews.My
{
    public class BeerReviewInfoEventArgs : EventArgs, IBeerReviewInfoEventArgs
    {
        public BeerReviewInfoEventArgs(IBeerReview beerReview)
        {
            this.BeerReview = beerReview;
        }

        public BeerReviewInfoEventArgs(IBeerReview beerReview, byte[] image)
            : this(beerReview)
        {
            this.Image = image;
        }

        public IBeerReview BeerReview { get; set; }

        public byte[] Image { get; set; }
    }
}
