using System;

using NUnit.Framework;

using RememBeer.Services;

namespace RememBeer.Tests.Services.BeerReviewServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            // Act & assert
            Assert.Throws<ArgumentNullException>(() => new BeerReviewService(null));
        }
    }
}
