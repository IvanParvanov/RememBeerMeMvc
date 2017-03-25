using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.Models.Factories;
using RememBeer.Services.RankingStrategies;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.RankingStrategies.DoubleOverallScoreStrategyTests
{
    [TestFixture]
    public class GetBreweryRank_Should : TestClassBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            this.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => this.Fixture.Behaviors.Remove(b));
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void ThrowArgumentNullException_WhenBeerRanksArgumentIsNull()
        {
            // Arrange
            var factory = new Mock<IModelFactory>();
            var name = this.Fixture.Create<string>();

            var strategy = new DoubleOverallScoreStrategy(factory.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => strategy.GetBreweryRank(null, name));
        }

        [TestCase(null)]
        [TestCase("")]
        public void ThrowArgumentNullException_WhenBreweryNameArgumentIsNullOrEmpty(string nullOrEmpty)
        {
            // Arrange
            var factory = new Mock<IModelFactory>();
            var reviews = new Mock<IEnumerable<IBeerRank>>();

            var strategy = new DoubleOverallScoreStrategy(factory.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => strategy.GetBreweryRank(reviews.Object, nullOrEmpty));
        }

        [Test]
        public void CallFactoryCreateBreweryRankMethodWithCorrectParamsOnceAndReturnItsValue()
        {
            // Arrange
            var expectedName = this.Fixture.Create<string>();
            var expectedBreweryRank = new Mock<IBreweryRank>();
            var totalRankCount = 5;
            var beerRanks = new List<IBeerRank>();
            var factory = new Mock<IRankFactory>();
            for (int i = 0; i < totalRankCount; i++)
            {
                var reviews = this.Fixture.Create<List<BeerReview>>();
                var mockedBeer = new Mock<IBeer>();
                mockedBeer.Setup(b => b.Reviews)
                          .Returns(reviews);

                var mockedRank = new Mock<IBeerRank>();
                mockedRank.Setup(r => r.CompositeScore)
                          .Returns(this.Fixture.Create<decimal>());
                mockedRank.Setup(r => r.Beer)
                          .Returns(mockedBeer.Object);
                beerRanks.Add(mockedRank.Object);
            }

            var expectedTotalScore = beerRanks.Sum(s => s.CompositeScore) / totalRankCount;
            var expectedTotalReviews = beerRanks.Sum(b => b.Beer.Reviews.Count);
            factory.Setup(f => f.CreateBreweryRank(expectedTotalScore, expectedTotalReviews, expectedName))
                   .Returns(expectedBreweryRank.Object);
            var strategy = new DoubleOverallScoreStrategy(factory.Object);

            // Act
            var result = strategy.GetBreweryRank(beerRanks, expectedName);

            // Assert
            factory.Verify(f => f.CreateBreweryRank(expectedTotalScore, expectedTotalReviews, expectedName), Times.Once);
            Assert.AreSame(expectedBreweryRank.Object, result);
        }
    }
}
