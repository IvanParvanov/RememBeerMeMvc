using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Business.Logic.Admin.Breweries;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Business.Logic.Admin.Breweries.ViewModel
{
    [TestFixture]
    public class Setters_Should
    {
        [Test]
        public void SetUpPropertiesCorrectly()
        {
            var expected = new List<IBrewery>();

            var viewModel = new BreweriesViewModel();
            viewModel.Breweries = expected;

            Assert.AreSame(expected, viewModel.Breweries);
        }
    }
}
