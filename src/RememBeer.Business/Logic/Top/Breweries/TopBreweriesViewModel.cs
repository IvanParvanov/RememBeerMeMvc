using System.Collections.Generic;

using RememBeer.Models.Dtos;

namespace RememBeer.Business.Logic.Top.Breweries
{
    public class TopBreweriesViewModel
    {
        public virtual IEnumerable<IBreweryRank> Rankings { get; set; }
    }
}
