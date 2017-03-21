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
            // Arrange
            var strategy = new Mock<IRankCalculationStrategy>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TopBeersService(null, strategy.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenStrategyIsNull()
        {
            // Arrange
            var repo = new Mock<IEfRepository<BeerReview>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TopBeersService(repo.Object, null));
        }
    }
}
