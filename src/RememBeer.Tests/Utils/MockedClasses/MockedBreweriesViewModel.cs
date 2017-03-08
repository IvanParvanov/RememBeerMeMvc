using System.Collections.Generic;

using RememBeer.Business.Logic.Admin.Breweries;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedBreweriesViewModel : BreweriesViewModel
    {
        public override IEnumerable<IBrewery> Breweries { get; set; }
    }
}
