using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedSingleBreweryViewModel : SingleBreweryViewModel
    {
        public override IBrewery Brewery { get; set; }
    }
}
