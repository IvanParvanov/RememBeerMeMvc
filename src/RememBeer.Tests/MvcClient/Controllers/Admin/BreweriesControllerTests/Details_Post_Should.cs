using System;
using System.Net;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Data.Repositories;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Areas.Admin.Models;
using RememBeer.MvcClient.Filters;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Details_Post_Should : BreweriesControllerTestBase
    {
        [TestCase(typeof(AjaxOnlyAttribute))]
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Details(It.IsAny<CreateBeerBindingModel>()), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(0, 0, null)]
        [TestCase(-1, -1, "")]
        [TestCase(991, 997, "asdkasdj89u898700545209æÔ")]
        public void Call_ReviewServiceAddBeerMethodOnceWithCorrectParams(int expectedId, int expectedType, string expectedName)
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.AddNewBeer(expectedId, expectedType, expectedName))
                          .Returns(new Mock<IDataModifiedResult>().Object);

            var model = new CreateBeerBindingModel()
                        {
                            Id = expectedId,
                            TypeId = expectedType,
                            BeerName = expectedName
                        };
            // Act
            sut.Details(model);

            // Assert
            breweryService.Verify(s => s.AddNewBeer(expectedId, expectedType, expectedName), Times.Once);
        }

        [Test]
        public void Return_CorrectResult_WhenCreationFails()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.AddNewBeer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                          .Returns(new Mock<IDataModifiedResult>().Object);

            // Act
            var result = sut.Details(new CreateBeerBindingModel()) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            StringAssert.Contains("validation failed", result.StatusDescription);
        }

        [Test]
        public void Return_CorrectResult_WhenCreationSucceeds()
        {
            // Arrange
            var sut = this.MockingKernel.Get<BreweriesController>();
            var dataResult = new Mock<IDataModifiedResult>();
            dataResult.Setup(r => r.Successful)
                      .Returns(true);
            var breweryService = this.MockingKernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.AddNewBeer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                          .Returns(dataResult.Object);
            // Act
            var result = sut.Details(new CreateBeerBindingModel() {Id=15}) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(15, result.RouteValues["id"]);
        }
    }
}
