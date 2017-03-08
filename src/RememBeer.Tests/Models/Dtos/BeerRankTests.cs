using System;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Models.Dtos
{
    [TestFixture]
    public class BeerRankTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetPropertiesCorrectly()
        {
            var overallScore = this.Fixture.Create<decimal>();
            var tasteScore = this.Fixture.Create<decimal>();
            var looksScore = this.Fixture.Create<decimal>();
            var smellScore = this.Fixture.Create<decimal>();
            var totalReviews = this.Fixture.Create<int>();
            var compositeScore = this.Fixture.Create<decimal>();
            var beer = new Mock<IBeer>();

            var rank = new BeerRank(overallScore,
                                    tasteScore,
                                    looksScore,
                                    smellScore,
                                    beer.Object,
                                    compositeScore,
                                    totalReviews);

            Assert.AreEqual(overallScore, rank.OverallScore);
            Assert.AreEqual(tasteScore, rank.TasteScore);
            Assert.AreEqual(looksScore, rank.LookScore);
            Assert.AreEqual(smellScore, rank.SmellScore);
            Assert.AreEqual(compositeScore, rank.CompositeScore);
            Assert.AreEqual(totalReviews, rank.TotalReviews);
            Assert.AreSame(beer.Object, rank.Beer);
        }

        [Test]
        public void Ctor_ShouldThrowArgumenNullException_WhenBeerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BeerRank(0, 0, 0, 0, null, 0, 0));
        }
    }
}
