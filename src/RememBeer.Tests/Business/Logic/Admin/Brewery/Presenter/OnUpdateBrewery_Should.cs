using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.Presenter
{
    [TestFixture]
    public class OnUpdateBrewery_Should : TestClassBase
    {
        [Test]
        public void CallUpdateBreweryMethodOnceWithCorrectParams()
        {
            var expectedId = this.Fixture.Create<int>();
            var expectedName = this.Fixture.Create<string>();
            var expectedDescr = this.Fixture.Create<string>();
            var expectedCountry = this.Fixture.Create<string>();

            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IBreweryUpdateEventArgs>();
            args.Setup(a => a.Id).Returns(expectedId);
            args.Setup(a => a.Description).Returns(expectedDescr);
            args.Setup(a => a.Country).Returns(expectedCountry);
            args.Setup(a => a.Name).Returns(expectedName);

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(true);

            var service = new Mock<IBreweryService>();
            service.Setup(s => s.UpdateBrewery(expectedId, expectedName, expectedCountry, expectedDescr))
                   .Returns(result.Object);

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.BreweryUpdate += null, view.Object, args.Object);

            service.Verify(s => s.UpdateBrewery(expectedId, expectedName, expectedCountry, expectedDescr), Times.Once);
        }

        [Test]
        public void SetViewsSuccessMessageVisibilityAndText()
        {
            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IBreweryUpdateEventArgs>();

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Successful).Returns(true);

            var service = new Mock<IBreweryService>();
            service.Setup(s =>
                              s.UpdateBrewery(It.IsAny<int>(),
                                              It.IsAny<string>(),
                                              It.IsAny<string>(),
                                              It.IsAny<string>()))
                   .Returns(result.Object);

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.BreweryUpdate += null, view.Object, args.Object);

            view.VerifySet(v => v.SuccessMessageText = It.IsAny<string>(), Times.Once);
            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
        }

        [Test]
        public void CatchExceptionAndSetViewsErrorMessageCorrectly()
        {
            var expectedMessage = this.Fixture.Create<string>();

            var viewModel = new MockedSingleBreweryViewModel();
            var view = new Mock<ISingleBreweryView>();
            view.Setup(v => v.Model)
                .Returns(viewModel);

            var args = new Mock<IBreweryUpdateEventArgs>();

            var result = new Mock<IDataModifiedResult>();
            result.Setup(r => r.Errors).Returns(new[] { expectedMessage });
            result.Setup(r => r.Successful).Returns(false);

            var service = new Mock<IBreweryService>();
            service.Setup(s =>
                              s.UpdateBrewery(It.IsAny<int>(),
                                              It.IsAny<string>(),
                                              It.IsAny<string>(),
                                              It.IsAny<string>()))
                   .Returns(result.Object);

            var presenter = new BreweryPresenter(service.Object, view.Object);
            view.Raise(v => v.BreweryUpdate += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageText = expectedMessage, Times.Once);
            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
        }
    }
}
