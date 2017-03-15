using System;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;

namespace RememBeer.Tests.Mvc.Controllers.TopControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenTopBeerServiceIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TopController(null));
        }
    }
}
