using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Business.Logic.Top.Breweries;
using RememBeer.Models.Dtos;

namespace RememBeer.Tests.Business.Logic.Top.Breweries.ViewModel
{
    [TestFixture]
    public class TopBreweriesViewModelTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            var expedtedRankings = new List<IBreweryRank>();
            var sut = new TopBreweriesViewModel();

            sut.Rankings = expedtedRankings;

            Assert.AreEqual(expedtedRankings, sut.Rankings);
        }
    }
}
