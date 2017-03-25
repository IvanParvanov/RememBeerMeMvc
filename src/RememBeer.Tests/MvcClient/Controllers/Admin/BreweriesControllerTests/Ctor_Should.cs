using System;

using AutoMapper;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Class_ShouldHaveAdminAuthorizeAttribute()
        {
            // Act & Assert
            AttributeTester.EnsureClassHasAdminAuthorizationAttribute(typeof(BreweriesController));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIMapperIsNull()
        {
            // Arrange
            var breweryService = new Mock<IBreweryService>();
            var beerTypesService = new Mock<IBeerTypesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BreweriesController(null, breweryService.Object, beerTypesService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBreweryServiceIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var beerTypesService = new Mock<IBeerTypesService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BreweriesController(mapper.Object, null, beerTypesService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBeerTypesServiceIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var breweryService = new Mock<IBreweryService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BreweriesController(mapper.Object, breweryService.Object, null));
        }
    }
}
