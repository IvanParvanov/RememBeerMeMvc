using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Areas.Admin.Models;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Details_Get_Should : BreweriesControllerTestBase
    {
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(991)]
        public void Call_BreweryServiceGetByIdMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();

            // Act
            sut.Details(expectedId);

            // Assert
            breweryService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams()
        {
            // Arrange
            var expectedBrewery = new Mock<IBrewery>();
            var sut = this.MockingKernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.GetById(It.IsAny<int>()))
                          .Returns(expectedBrewery.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();

            // Act
            sut.Details(It.IsAny<int>());

            // Assert
            mapper.Verify(m => m.Map<IBrewery, BreweryDetailsViewModel>(expectedBrewery.Object), Times.Once);
        }

        [Test]
        public void Return_CorrectPartialViewResult_WhenRequestIsAjax()
        {
            // Arrange
            var expectedModel = new BreweryDetailsViewModel();
            var sut = this.MockingKernel.Get<BreweriesController>(AjaxContextName);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBrewery, BreweryDetailsViewModel>(It.IsAny<IBrewery>()))
                  .Returns(expectedModel);

            // Act
            var result = sut.Details(It.IsAny<int>()) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Model as BreweryDetailsViewModel;
            Assert.AreSame(expectedModel, actual);
            StringAssert.Contains("_Details", result.ViewName);
        }

        [Test]
        public void Return_CorrectViewResult_WhenRequestIsNotAjax()
        {
            // Arrange
            var expectedModel = new BreweryDetailsViewModel();
            var sut = this.MockingKernel.Get<BreweriesController>(RegularContextName);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBrewery, BreweryDetailsViewModel>(It.IsAny<IBrewery>()))
                  .Returns(expectedModel);

            // Act
            var result = sut.Details(It.IsAny<int>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Model as BreweryDetailsViewModel;
            Assert.AreSame(expectedModel, actual);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
