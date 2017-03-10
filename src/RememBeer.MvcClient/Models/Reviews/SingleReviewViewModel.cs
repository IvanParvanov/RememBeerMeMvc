using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class SingleReviewViewModel : EditReviewBindingModel
    {
        public string ImgUrl { get; set; }

        [DisplayName("Beer Name")]
        public string BeerName { get; set; }

        [DisplayName("Style")]
        public string BeerBeerTypeType { get; set; }

        [DisplayName("Brewery")]
        public string BeerBreweryName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsEdit { get; set; }
    }
}
