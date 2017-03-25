using System;
using System.Net;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Areas.Admin.Models;
using RememBeer.MvcClient.Filters;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Details_Put_Should : BreweriesControllerTestBase
    {
        [TestCase(typeof(AjaxOnlyAttribute))]
        [TestCase(typeof(HttpPutAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Details(It.IsAny<EditBreweryBindingModel>()), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Return_HttpStatusCode_WhenModelStateIsInvalid()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            sut.InvalidateViewModel();

            // Act
            var result = sut.Details(It.IsAny<EditBreweryBindingModel>()) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            StringAssert.Contains("validation failed", result.StatusDescription);
        }

        [TestCase(1, "BG", "kjsalkjdask", "asioiouiouqejklhasjkl@*(718978912389127389")]
        [TestCase(1, "asdasdasdasBG", "kjsaasdlkjdask", "asioiouiouqejkaasdlhasjkl@*(71897891as2389127389")]
        [TestCase(0, null, null, null)]
        public void Call_UpdateBreweryMethodOnceWithCorrectParams_WhenModelIsValid(int expectedId, string expectedCountry, string expectedName, string expectedDescription)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var result = new Mock<IDataModifiedResult>();
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.UpdateBrewery(expectedId, expectedName, expectedCountry, expectedDescription))
                          .Returns(result.Object);
            var model = new EditBreweryBindingModel()
                        {
                            Id = expectedId,
                            Country = expectedCountry,
                            Name = expectedName,
                            Description = expectedDescription
                        };

            // Act
            sut.Details(model);

            // Assert
            breweryService.Verify(s => s.UpdateBrewery(expectedId, expectedName, expectedCountry, expectedDescription), Times.Once);
        }

        [Test]
        public void Return_CorrectResult_WhenUpdateFails()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(r => r.Successful)
                      .Returns(false);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.UpdateBrewery(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(dataResult.Object);

            // Act
            var result = sut.Details(new EditBreweryBindingModel()) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            StringAssert.Contains("validation failed", result.StatusDescription);
        }

        [TestCase(991)]
        [TestCase(-991)]
        public void Call_GetByIdMethodOnceWithCorrectParams_WhenUpdateIsSuccessful(int expectedId)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var model = new EditBreweryBindingModel() { Id = expectedId };
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(r => r.Successful)
                      .Returns(true);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.UpdateBrewery(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(dataResult.Object);

            // Act
            sut.Details(model);

            // Assert
            breweryService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Call_MapOnceWithCorrectParams_WhenUpdateIsSuccessful()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var model = new EditBreweryBindingModel();
            var expectedBrewery = new Mock<IBrewery>();
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(r => r.Successful)
                      .Returns(true);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.UpdateBrewery(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(dataResult.Object);
            breweryService.Setup(s => s.GetById(It.IsAny<int>()))
                          .Returns(expectedBrewery.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();

            // Act
            sut.Details(model);

            // Assert
            mapper.Verify(m => m.Map<IBrewery, BreweryDetailsViewModel>(expectedBrewery.Object), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var model = new EditBreweryBindingModel();
            var expected = new BreweryDetailsViewModel();
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(r => r.Successful)
                      .Returns(true);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.UpdateBrewery(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(dataResult.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map<IBrewery, BreweryDetailsViewModel>(It.IsAny<IBrewery>()))
                  .Returns(expected);

            // Act
            var result = sut.Details(model) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("_Details", result.ViewName);
            var actual = result.Model as BreweryDetailsViewModel;
            Assert.AreSame(expected, actual);
        }
    }
}
