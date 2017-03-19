using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Types_Should : BreweriesControllerTestBase
    {
        [Test]
        public void Have_AjaxOnlyAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Types(It.IsAny<string>()), typeof(AjaxOnlyAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("l;kasdl;kasdopi123908231908asd90uasdhasdkjhnasdkjlasd")]
        public void Call_BeerTypesServiceSearchMethodOnceWithCorrectParams(string search)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var beerTypesService = this.MockingKernel.GetMock<IBeerTypesService>();

            // Act
            sut.Types(search);

            // Assert
            beerTypesService.Verify(s => s.Search(search), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var expectedTypes = new List<IBeerType>();
            var beerTypesService = this.MockingKernel.GetMock<IBeerTypesService>();
            beerTypesService.Setup(s => s.Search(It.IsAny<string>()))
                            .Returns(expectedTypes);
            var mapper = this.MockingKernel.GetMock<IMapper>();

            // Act
            sut.Types(It.IsAny<string>());

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeerType>, IEnumerable<BeerTypeDto>>(expectedTypes), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var expectedTypes = new List<IBeerType>();
            var expectedDtos = new List<BeerTypeDto>();
            var beerTypesService = this.MockingKernel.GetMock<IBeerTypesService>();
            beerTypesService.Setup(s => s.Search(It.IsAny<string>()))
                            .Returns(expectedTypes);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeerType>, IEnumerable<BeerTypeDto>>(expectedTypes))
                  .Returns(expectedDtos);

            // Act
            var result = sut.Types(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.JsonRequestBehavior, JsonRequestBehavior.AllowGet);
            Assert.AreSame(expectedDtos, (result.Data as dynamic).data);
        }
    }
}
