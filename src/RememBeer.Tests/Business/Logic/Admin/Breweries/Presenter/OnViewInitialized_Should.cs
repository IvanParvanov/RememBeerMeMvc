using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Admin.Breweries;
using RememBeer.Business.Logic.Admin.Breweries.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Admin.Breweries.Presenter
{
    [TestFixture]
    public class OnViewInitialized_Should : TestClassBase
    {
        [Test]
        public void CallBreweryServiceGetAllMethodOnce()
        {
            var viewModel = new MockedBreweriesViewModel();
            var view = new Mock<IBreweriesView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var service = new Mock<IBreweryService>();

            var presenter = new BreweriesPresenter(service.Object, view.Object);

            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            service.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void SetResultFromServiceToViewModel()
        {
            var expectedBreweries = new List<IBrewery>();

            var viewModel = new MockedBreweriesViewModel();
            var view = new Mock<IBreweriesView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var service = new Mock<IBreweryService>();
            service.Setup(s => s.GetAll())
                   .Returns(expectedBreweries);
            var presenter = new BreweriesPresenter(service.Object, view.Object);

            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            Assert.AreSame(expectedBreweries, view.Object.Model.Breweries);
        }
    }
}
