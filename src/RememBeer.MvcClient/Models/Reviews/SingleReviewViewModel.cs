using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class SingleReviewViewModel
    {
        public string Id { get; set; }

        public string ImgUrl { get; set; }

        [DisplayName("Beer Name")]
        public string BeerName { get; set; }

        [DisplayName("Style")]
        public string BeerBeerTypeType { get; set; }

        [DisplayName("Brewery")]
        public string BeerBreweryName { get; set; }

        [DisplayName("Location")]
        public string Place { get; set; }

        public string Description { get; set; }

        public int Overall { get; set; }

        public int Taste { get; set; }

        [DisplayName("Aroma")]
        public int Smell { get; set; }

        [DisplayName("Looks")]
        public int Look { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.mm.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsEdit { get; set; }
    }
}
