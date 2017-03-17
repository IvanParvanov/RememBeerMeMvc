using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.BreweriesControllerTests
{
    [TestFixture]
    public class Index_Should : BreweriesControllerTestBase
    {
        [TestCase(0, 0, "")]
        [TestCase(991, 997, "asdasjkhasjk1289123378912378")]
        [TestCase(-1, -997, null)]
        public void Call_BreweryServiceGetAllMethodOnceWithCorrectParams(int page, int expectedPageSize, string search)
        {
            // Arrange
            var sut = this.Kernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.Kernel.GetMock<IBreweryService>();

            // Act
            sut.Index(page, expectedPageSize, search);

            // Assert
            breweryService.Verify(s => s.GetAll(page * expectedPageSize, expectedPageSize, It.IsAny<Func<IBrewery, int>>(), search), Times.Once());
        }

        [Test]
        public void Call_BreweryServiceCountAllMethodOnceWithCorrectParams_WhenSearchIsNotNull()
        {
            // Arrange
            var expectedSearch = Guid.NewGuid().ToString();
            var sut = this.Kernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.Kernel.GetMock<IBreweryService>();

            // Act
            sut.Index(1, 1, expectedSearch);

            // Assert
            breweryService.Verify(s => s.CountAll(expectedSearch), Times.Once());
        }

        [Test]
        public void Call_BreweryServiceCountAllMethodOnceWithNoParams_WhenSearchIsNull()
        {
            // Arrange
            var sut = this.Kernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.Kernel.GetMock<IBreweryService>();

            // Act
            sut.Index(1, 1, null);

            // Assert
            breweryService.Verify(s => s.CountAll(), Times.Once());
        }

        [Test]
        public void Return_CorrectPartialViewResult_WhenRequestIsAjax()
        {
            // Arrange
            var expectedBreweries = new List<IBrewery>();
            var sut = this.Kernel.Get<BreweriesController>(AjaxContextName);
            var breweryService = this.Kernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Func<IBrewery, int>>(), It.IsAny<string>()))
                          .Returns(expectedBreweries);

            // Act
            var result = sut.Index() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("_BreweryList", result.ViewName);
            var actual = result.Model as PaginatedViewModel<IBrewery>;
            Assert.IsNotNull(actual);
            Assert.AreSame(expectedBreweries, actual.Items);
        }

        [Test]
        public void Return_CorrectViewResult_WhenRequestIsNotAjax()
        {
            // Arrange
            var expectedBreweries = new List<IBrewery>();
            var sut = this.Kernel.Get<BreweriesController>(RegularContextName);
            var breweryService = this.Kernel.GetMock<IBreweryService>();
            breweryService.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Func<IBrewery, int>>(), It.IsAny<string>()))
                          .Returns(expectedBreweries);

            // Act
            var result = sut.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Model as PaginatedViewModel<IBrewery>;
            Assert.IsNotNull(actual);
            Assert.AreSame(expectedBreweries, actual.Items);
        }
    }
}
