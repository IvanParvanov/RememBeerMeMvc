using System;

using NUnit.Framework;

using RememBeer.Services.RankingStrategies;

namespace RememBeer.Tests.Services.RankingStrategies.DoubleOverallScoreStrategyTests
{
    [TestFixture]
    class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenArgumentIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DoubleOverallScoreStrategy(null));
        }
    }
}
