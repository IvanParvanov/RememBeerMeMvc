using System;

namespace RememBeer.MvcClient.Models
{
    public class SingleReviewViewModel
    {
        public string Id { get; set; }

        public string ImgUrl { get; set; }

        public string BeerName { get; set; }

        public string BeerBeerTypeType { get; set; }

        public string BeerBreweryName { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public int Overall { get; set; }

        public int Taste { get; set; }

        public int Smell { get; set; }

        public int Look { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsEdit { get; set; }
    }
}
