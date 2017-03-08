using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.Presenter
{
    [TestFixture]
    public class OnViewBreweryAddBeer_Should : TestClassBase
    {
        [Test]
        public void Call_BreweryServiceAddNewBeerMethodOnceWithCorrectParams()
        {
            var result = new Mock<IDataModifiedResult>();
            var breweryId = this.Fixture.Create<int>();
            var typeId = this.Fixture.Create<int>();
            var beerName = this.Fixture.Create<string>();
            var args = new Mock<ICreateBeerEventArgs>();
            args.Setup(a => a.Id).Returns(breweryId);
            args.Setup(a => a.BeerTypeId).Returns(typeId);
            args.Setup(a => a.BeerName).Returns(beerName);

            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.AddNewBeer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                          .Returns(result.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryAddBeer += null, view.Object, args.Object);

            breweryService.Verify(b => b.AddNewBeer(breweryId, typeId, beerName), Times.Once);
        }

        [Test]
        public void Set_ViewPropertiesCorrectly_WhenResultIsSuccessful()
        {
            var expectedResult = new Mock<IDataModifiedResult>();
            expectedResult.Setup(r => r.Successful).Returns(true);

            var args = new Mock<ICreateBeerEventArgs>();
            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.AddNewBeer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                          .Returns(expectedResult.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryAddBeer += null, view.Object, args.Object);

            view.VerifySet(v=> v.SuccessMessageVisible = true, Times.Once);
            view.VerifySet(v=> v.SuccessMessageText = It.IsAny<string>(), Times.Once);
        }

        [Test]
        public void Set_ViewPropertiesCorrectly_WhenResultFails()
        {
            var expectedResult = new Mock<IDataModifiedResult>();
            expectedResult.Setup(r => r.Successful).Returns(false);

            var args = new Mock<ICreateBeerEventArgs>();
            var view = new Mock<ISingleBreweryView>();
            var breweryService = new Mock<IBreweryService>();
            breweryService.Setup(b => b.AddNewBeer(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                          .Returns(expectedResult.Object);
            var presenter = new BreweryPresenter(breweryService.Object, view.Object);

            view.Raise(v => v.BreweryAddBeer += null, view.Object, args.Object);

            view.VerifySet(v => v.ErrorMessageVisible = true, Times.Once);
            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>(), Times.Once);
        }
    }
}
