using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.MvpPresenterFactory;

using WebFormsMvp;

namespace RememBeer.Tests.Business.Logic.MvpPresenterFactory
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallGetPresenterWithCorrectParameters()
        {
            var mockedFactory = new Mock<IMvpPresenterFactory>();
            var mockedView = new Mock<IView>();
            var expectedPresenterType = typeof(IPresenter);
            var sut = new RememBeer.Business.Logic.MvpPresenterFactory.MvpPresenterFactory(mockedFactory.Object);

            sut.Create(expectedPresenterType, expectedPresenterType, mockedView.Object);

            mockedFactory.Verify(f => f.GetPresenter(expectedPresenterType, mockedView.Object), Times.Once());
        }

        [Test]
        public void ReturnValueFromFactory()
        {
            var mockedView = new Mock<IView>();
            var expectedPresenterType = typeof(IPresenter);
            var mockedPresenter = new Mock<IPresenter>();

            var mockedFactory = new Mock<IMvpPresenterFactory>();
            mockedFactory.Setup(f => f.GetPresenter(expectedPresenterType, mockedView.Object))
                         .Returns(mockedPresenter.Object);

            var sut = new RememBeer.Business.Logic.MvpPresenterFactory.MvpPresenterFactory(mockedFactory.Object);

            var result = sut.Create(expectedPresenterType, expectedPresenterType, mockedView.Object);

            Assert.AreSame(mockedPresenter.Object, result);
        }
    }
}
