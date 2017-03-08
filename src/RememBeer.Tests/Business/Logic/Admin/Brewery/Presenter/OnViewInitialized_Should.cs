using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.Presenter
{
    [TestFixture]
    public class OnViewInitialized_Should : TestClassBase
    {
        [Test]
        public void ShowErrors_WhenIdIsInvalid()
        {
            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id).Returns("thisshouldalwaysbeinvalidID" + this.Fixture.Create<string>());

            var service = new Mock<IBreweryService>();

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>(), Times.Once);
            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
        }

        [Test]
        public void CallServiceGetByIdMethodOnceWithCorrectParams_WhenIdIsValid()
        {
            var expectedId = this.Fixture.Create<int>().ToString();
            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id).Returns(expectedId);

            var service = new Mock<IBreweryService>();

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            service.Verify(s => s.GetById(int.Parse(expectedId)), Times.Once);
        }

        [Test]
        public void ShowErrors_ServiceDoesNotFindBrewery()
        {
            var expectedId = this.Fixture.Create<int>().ToString();
            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id).Returns(expectedId);

            var service = new Mock<IBreweryService>();
            service.Setup(s => s.GetById(It.IsAny<object>()))
                   .Returns((IBrewery)null);

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>(), Times.Once);
            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
        }

        [Test]
        public void SetViewModelProperties_WhenServiceFindsBrewery()
        {
            var expectedBrewery = new Mock<IBrewery>();
            var viewModel = new MockedSingleBreweryViewModel();

            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var expectedId = this.Fixture.Create<int>().ToString();
            var args = new Mock<IIdentifiableEventArgs<string>>();
            args.Setup(a => a.Id).Returns(expectedId);

            var service = new Mock<IBreweryService>();
            service.Setup(s => s.GetById(It.IsAny<object>()))
                   .Returns(expectedBrewery.Object);

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.Initialized += null, view.Object, args.Object);

            Assert.AreSame(expectedBrewery.Object, viewModel.Brewery);
        }
    }
}
