using System.Collections.Generic;

using RememBeer.Models;

namespace RememBeer.MvcClient.Areas.Admin.Models
{
    public class BreweryDetailsViewModel
    {
        public CreateBeerBindingModel CreateModel { get; set; }

        public EditBreweryBindingModel EditModel { get; set; }

        public ICollection<Beer> Beers { get; set; }
    }
}
