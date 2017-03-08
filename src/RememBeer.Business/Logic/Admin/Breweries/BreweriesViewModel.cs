using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Admin.Breweries
{
    public class BreweriesViewModel
    {
        public virtual IEnumerable<IBrewery> Breweries { get; set; }
    }
}
