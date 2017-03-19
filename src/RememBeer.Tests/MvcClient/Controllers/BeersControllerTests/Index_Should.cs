using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;
using Ninject.MockingKernel;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject.Base;

namespace RememBeer.Tests.MvcClient.Controllers.BeersControllerTests
{
    [TestFixture]
    public class Index_Should : NinjectTestBase
    {
        [TestCase("")]
        [TestCase(null)]
        [TestCase("io89712938iouiouasdjk2@#@askjdsklasjaslkdjio1289123798")]
        public void Call_BeerServiceSearchMethodOnceWithCorrectParams(string search)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BeersController>();
            var beerService = this.MockingKernel.GetMock<IBeerService>();

            // Act
            sut.Index(search);

            // Assert
            beerService.Verify(s => s.SearchBeers(search), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BeersController>();
            var expected = new List<IBeer>();
            var beerService = this.MockingKernel.GetMock<IBeerService>();
            beerService.Setup(s => s.SearchBeers(It.IsAny<string>()))
                       .Returns(expected);
            var mapper = this.MockingKernel.GetMock<IMapper>();

            // Act
            sut.Index("");

            // Assert
            mapper.Verify(m => m.Map<IEnumerable<IBeer>, IEnumerable<BeerDto>>(expected), Times.Once);
        }

        [Test]
        public void Return_CorrectJsonResult()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BeersController>();
            var expected = new List<BeerDto>();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<IBeer>, IEnumerable<BeerDto>>(It.IsAny<IEnumerable<IBeer>>()))
                .Returns(expected);

            // Act
            var result = sut.Index("");

            // Assert
            var actual = result.Data as IEnumerable<BeerDto>;
            Assert.AreSame(expected, actual);
            Assert.AreEqual(JsonRequestBehavior.AllowGet, result.JsonRequestBehavior);

        }

        public override void Init()
        {
            this.MockingKernel.Bind<BeersController>().ToSelf();

            this.MockingKernel.Bind<IMapper>().ToMock().InSingletonScope();
            this.MockingKernel.Bind<IBeerService>().ToMock().InSingletonScope();
        }
    }
}
