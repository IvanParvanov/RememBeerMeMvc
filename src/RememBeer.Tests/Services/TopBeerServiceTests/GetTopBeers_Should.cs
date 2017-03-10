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
    public class GetTopBeers_Should : TestClassBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            this.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => this.Fixture.Behaviors.Remove(b));
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void CallGetRankForEachReviewGroupWithSameBeer()
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

            var expectedGroups = reviews.Where(r => !r.IsDeleted).GroupBy(r => r.Beer);
            var enumeratedGroups = expectedGroups as IGrouping<Beer, BeerReview>[] ?? expectedGroups.ToArray();
            for (var i = 0; i < enumeratedGroups.Length; i++)
            {
                var rank = new Mock<IBeerRank>();
                rank.SetupGet(r => r.CompositeScore)
                    .Returns(i);
                strategy.Setup(s => s.GetBeerRank(enumeratedGroups[i], enumeratedGroups[i].Key))
                        .Returns(rank.Object);
            }

            var repository = new Mock<IRepository<BeerReview>>();
            repository.SetupGet(r => r.All)
                      .Returns(reviews.AsQueryable());

            var topBeersService = new TopBeersService(repository.Object, strategy.Object);

            // Act
            topBeersService.GetTopBeers(10);

            // Assert
            foreach (var expectedGroup in enumeratedGroups )
            {
                strategy.Verify(s => s.GetBeerRank(expectedGroup, expectedGroup.Key), Times.Once);
            }
        }

        [TestCase(15, 10)]
        [TestCase(10, 10)]
        [TestCase(8, 10)]
        public void ReturnCorrectNumberOfRanks(int totalReviews, int expectedCount)
        {
            // Arrange
            var reviews = new List<BeerReview>();
            var strategy = new Mock<IRankCalculationStrategy>();
            for (var i = 0; i < totalReviews; i++)
            {
                var rv = this.Fixture.Create<BeerReview>();
                reviews.Add(rv);
            }

            var expectedGroups = reviews.Where(r => !r.IsDeleted).GroupBy(r => r.Beer);
            var enumeratedGroups = expectedGroups as IGrouping<Beer, BeerReview>[] ?? expectedGroups.ToArray();
            for (var i = 0; i < enumeratedGroups.Length; i++)
            {
                var rank = new Mock<IBeerRank>();
                rank.SetupGet(r => r.CompositeScore)
                    .Returns(i);
                strategy.Setup(s => s.GetBeerRank(enumeratedGroups[i], enumeratedGroups[i].Key))
                        .Returns(rank.Object);
            }

            var repository = new Mock<IRepository<BeerReview>>();
            repository.SetupGet(r => r.All)
                      .Returns(reviews.AsQueryable());

            var topBeersService = new TopBeersService(repository.Object, strategy.Object);

            // Act
            var result = topBeersService.GetTopBeers(expectedCount);

            // Assert
            Assert.GreaterOrEqual(expectedCount, result.Count());
        }

        [TestCase(15, 10)]
        [TestCase(10, 10)]
        [TestCase(8, 10)]
        public void ReturnRanksOrderedByDescendingCompositeScore(int totalReviews, int expectedCount)
        {
            // Arrange
            var reviews = new List<BeerReview>();
            var strategy = new Mock<IRankCalculationStrategy>();
            for (var i = 0; i < totalReviews; i++)
            {
                var rv = this.Fixture.Create<BeerReview>();
                reviews.Add(rv);
            }

            var expectedGroups = reviews.Where(r => !r.IsDeleted).GroupBy(r => r.Beer);
            var enumeratedGroups = expectedGroups as IGrouping<Beer, BeerReview>[] ?? expectedGroups.ToArray();
            for (var i = 0; i < enumeratedGroups.Length; i++)
            {
                var rank = new Mock<IBeerRank>();
                rank.SetupGet(r => r.CompositeScore)
                    .Returns(i);
                strategy.Setup(s => s.GetBeerRank(enumeratedGroups[i], enumeratedGroups[i].Key))
                        .Returns(rank.Object);
            }

            var repository = new Mock<IRepository<BeerReview>>();
            repository.SetupGet(r => r.All)
                      .Returns(reviews.AsQueryable());

            var topBeersService = new TopBeersService(repository.Object, strategy.Object);

            // Act
            var result = topBeersService.GetTopBeers(expectedCount);

            // Assert
            var comparer = Comparer<IBeerRank>.Create((a, b) => b.CompositeScore.CompareTo(a.CompositeScore));
            CollectionAssert.IsOrdered(result, comparer);
        }
    }
}
