using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Breweries;
using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Top.Breweries.Presenter
{
    [TestFixture]
    public class OnViewInitialize_Should
    {
        [Test]
        public void CallGetTopBreweriesWithCorrectParametersOnce()
        {
            var viewModel = new MockedTopBreweriesViewModel();
            var view = new Mock<IInitializableView<TopBreweriesViewModel>>();
            view.Setup(v => v.Model).Returns(viewModel);
            var service = new Mock<ITopBeersService>();

            var presenter = new TopBreweriesPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            service.Verify(s => s.GetTopBreweries(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void SetModelRankingsToReturnValueOfGetTopBeers()
        {
            var expectedResult = new List<IBreweryRank>();

            var viewModel = new MockedTopBreweriesViewModel();
            var view = new Mock<IInitializableView<TopBreweriesViewModel>>();
            view.Setup(v => v.Model).Returns(viewModel);

            var service = new Mock<ITopBeersService>();
            service.Setup(s => s.GetTopBreweries(It.IsAny<int>())).Returns(expectedResult);

            var presenter = new TopBreweriesPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            Assert.AreSame(viewModel.Rankings, expectedResult);
        }
    }
}
