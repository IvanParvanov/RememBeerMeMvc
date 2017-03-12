using System;

using NUnit.Framework;

using RememBeer.Services;

namespace RememBeer.Tests.Services.BeerServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenRepositoryIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BeerService(null));
        }
    }
}
