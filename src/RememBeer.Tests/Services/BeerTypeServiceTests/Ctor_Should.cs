using System;

using NUnit.Framework;

using RememBeer.Services;

namespace RememBeer.Tests.Services.BeerTypeServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BeerTypesService(null));
        }
    }
}
