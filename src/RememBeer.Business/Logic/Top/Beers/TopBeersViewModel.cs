using System.Collections.Generic;

using RememBeer.Models.Dtos;

namespace RememBeer.Business.Logic.Top.Beers
{
    public class TopBeersViewModel
    {
        public virtual IEnumerable<IBeerRank> Rankings { get; set; }
    }
}
