using System;

using Moq;

using NUnit.Framework;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Services.RankingStrategies.Contracts;

namespace RememBeer.Tests.Services.TopBeerServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            var strategy = new Mock<IRankCalculationStrategy>();
            Assert.Throws<ArgumentNullException>(() => new TopBeersService(null, strategy.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenStrategyIsNull()
        {
            var repo = new Mock<IRepository<BeerReview>>();
            Assert.Throws<ArgumentNullException>(() => new TopBeersService(repo.Object, null));
        }
    }
}
