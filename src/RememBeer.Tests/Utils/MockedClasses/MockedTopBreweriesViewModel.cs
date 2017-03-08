using System.Collections.Generic;

using RememBeer.Business.Logic.Top.Breweries;
using RememBeer.Models.Dtos;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedTopBreweriesViewModel : TopBreweriesViewModel
    {
        public override IEnumerable<IBreweryRank> Rankings { get; set; }
    }
}
