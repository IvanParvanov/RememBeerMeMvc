using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Beers;
using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Top.Beers.Presenter
{
    [TestFixture]
    public class OnViewInitialize_Should
    {
        [Test]
        public void CallGetTopBeersWithCorrectParametersOnce()
        {
            var viewModel = new MockedTopBeersViewModel();
            var view = new Mock<IInitializableView<TopBeersViewModel>>();
            view.Setup(v => v.Model).Returns(viewModel);
            var service = new Mock<ITopBeersService>();

            var presenter = new TopBeersPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            service.Verify(s => s.GetTopBeers(Constants.TopBeersCount), Times.Once);
        }

        [Test]
        public void SetModelRankingsToReturnValueOfGetTopBeers()
        {
            var expectedResult = new List<IBeerRank>();

            var viewModel = new MockedTopBeersViewModel();
            var view = new Mock<IInitializableView<TopBeersViewModel>>();
            view.Setup(v => v.Model).Returns(viewModel);

            var service = new Mock<ITopBeersService>();
            service.Setup(s => s.GetTopBeers(It.IsAny<int>())).Returns(expectedResult);

            var presenter = new TopBeersPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, EventArgs.Empty);

            Assert.AreSame(viewModel.Rankings, expectedResult);
        }
    }
}
