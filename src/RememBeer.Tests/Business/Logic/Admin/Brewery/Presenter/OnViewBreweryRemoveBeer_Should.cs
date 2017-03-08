using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.Presenter
{
    [TestFixture]
    public class OnViewBreweryRemoveBeer_Should : TestClassBase
    {
        [Test]
        public void Call_BreweryServiceDeleteBeerMethodOnceWithCorrectParams()
        {
            var expectedId = this.Fixture.Create<int>();
            var expectedResult = new Mock<IDataModifiedResult>();
            expectedResult.Setup(r => r.Successful).Returns(false);
            var args = new Mock<IIdentifiableEventArgs<int>>();
            args.Setup(a => a.Id).Returns(expectedId);
            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.DeleteBeer(expectedId))
                          .Returns(expectedResult.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryRemoveBeer += null, view.Object, args.Object);

            breweryService.Verify(s => s.DeleteBeer(expectedId), Times.Once);
        }

        [Test]
        public void Set_ViewPropertiesCorrectly_WhenResultIsSuccessful()
        {
            var expectedResult = new Mock<IDataModifiedResult>();
            expectedResult.Setup(r => r.Successful).Returns(true);

            var args = new Mock<IIdentifiableEventArgs<int>>();
            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.DeleteBeer(It.IsAny<int>()))
                          .Returns(expectedResult.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryRemoveBeer += null, view.Object, args.Object);

            view.VerifySet(v => v.SuccessMessageVisible = true, Times.Once);
            view.VerifySet(v => v.SuccessMessageText = It.IsAny<string>(), Times.Once);
        }

        [Test]
        public void Set_ViewPropertiesCorrectly_WhenResultFails()
        {
            var expectedResult = new Mock<IDataModifiedResult>();
            expectedResult.Setup(r => r.Successful).Returns(false);

            var args = new Mock<IIdentifiableEventArgs<int>>();
            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.DeleteBeer(It.IsAny<int>()))
                          .Returns(expectedResult.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryRemoveBeer += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>(), Times.Once);
        }
    }
}
