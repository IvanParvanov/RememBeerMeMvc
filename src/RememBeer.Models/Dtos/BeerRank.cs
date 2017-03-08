using System;

using RememBeer.Models.Contracts;

namespace RememBeer.Models.Dtos
{
    public class BeerRank : IBeerRank
    {
        public BeerRank(decimal overallScore,
                        decimal tasteScore,
                        decimal lookScore,
                        decimal smellScore,
                        IBeer beer,
                        decimal compositeScore,
                        int totalReviews)
        {
            if (beer == null)
            {
                throw new ArgumentNullException(nameof(beer));
            }

            this.OverallScore = Math.Round(overallScore, 2);
            this.TasteScore = Math.Round(tasteScore, 2);
            this.LookScore = Math.Round(lookScore, 2);
            this.SmellScore = Math.Round(smellScore, 2);
            this.Beer = beer;
            this.CompositeScore = Math.Round(compositeScore, 2);
            this.TotalReviews = totalReviews;
        }

        public decimal OverallScore { get; set; }

        public decimal TasteScore { get; set; }

        public decimal LookScore { get; set; }

        public decimal SmellScore { get; set; }

        public IBeer Beer { get; set; }

        public decimal CompositeScore { get; set; }

        public int TotalReviews { get; set; }
    }
}
