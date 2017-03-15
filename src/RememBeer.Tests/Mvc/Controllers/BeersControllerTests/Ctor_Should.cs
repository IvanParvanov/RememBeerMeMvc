using System;

using AutoMapper;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Mvc.Controllers.BeersControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenMapperIsNull()
        {
            // Arrange
            var beerService = new Mock<IBeerService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BeersController(null, beerService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBeerServiceIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BeersController(mapper.Object, null));
        }
    }
}
