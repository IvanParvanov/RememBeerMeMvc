using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Models.Dtos;
using RememBeer.Services;
using RememBeer.Services.RankingStrategies.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.TopBeerServiceTests
{
    [TestFixture]
    public class GetTopBreweries_Should : TestClassBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            this.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => this.Fixture.Behaviors.Remove(b));
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void Call_StrategyGetBreweryRankForEachBeerRanking()
        {
            // Arrange
            var totalReviews = 15;
            var reviews = new List<BeerReview>();
            var strategy = new Mock<IRankCalculationStrategy>();
            for (var i = 0; i < totalReviews; i++)
            {
                var rv = this.Fixture.Create<BeerReview>();
                reviews.Add(rv);
            }

            var reviewRankingList = new List<IBeerRank>();

            var expectedGroups = reviews.Where(r => !r.IsDeleted).GroupBy(r => r.Beer);
            var beerRankingGroups = expectedGroups as IGrouping<Beer, BeerReview>[] ?? expectedGroups.ToArray();
            for (var i = 0; i < beerRankingGroups.Length; i++)
            {
                var rank = new Mock<IBeerRank>();
                var beer = this.Fixture.Create<Beer>();
                rank.SetupGet(r => r.CompositeScore)
                    .Returns(i);
                rank.SetupGet(r => r.Beer)
                    .Returns(beer);
                strategy.Setup(s => s.GetBeerRank(beerRankingGroups[i], beerRankingGroups[i].Key))
                        .Returns(rank.Object);
                reviewRankingList.Add(rank.Object);
            }

            var repository = new Mock<IRepository<BeerReview>>();
            repository.SetupGet(r => r.All)
                      .Returns(reviews.AsQueryable());

            var topBeersService = new TopBeersService(repository.Object, strategy.Object);

            // Act
            topBeersService.GetTopBreweries(10);

            // Assert
            var expectedGroupings = reviewRankingList.GroupBy(r => r.Beer.Brewery.Name);
            foreach (var expectedBreweryRankGroup in expectedGroupings)
            {
                strategy.Verify(s => s.GetBreweryRank(expectedBreweryRankGroup, expectedBreweryRankGroup.Key),
                                Times.Once);
            }
        }
    }
}
