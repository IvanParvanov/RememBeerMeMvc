using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.MvpPresenterFactory;
using RememBeer.Tests.Utils.MockedClasses;

using WebFormsMvp;

namespace RememBeer.Tests.Business.Logic.MvpPresenterFactory
{
    [TestFixture]
    public class Release_Should
    {
        [Test]
        public void CallDisposeOnPassedPresenter()
        {
            var mockedPresenter = new Mock<IDisposablePresenter>();
            var mockedFactory = new Mock<IMvpPresenterFactory>();

            var sut = new RememBeer.Business.Logic.MvpPresenterFactory.MvpPresenterFactory(mockedFactory.Object);

            sut.Release(mockedPresenter.Object);

            mockedPresenter.Verify(p => p.Dispose(), Times.Once());
        }

        [Test]
        public void NotThrow_WhenPresenterIsNotIDisposable()
        {
            var mockedPresenter = new Mock<IPresenter>();
            var mockedFactory = new Mock<IMvpPresenterFactory>();

            var sut = new RememBeer.Business.Logic.MvpPresenterFactory.MvpPresenterFactory(mockedFactory.Object);

            Assert.DoesNotThrow(() => sut.Release(mockedPresenter.Object));
        }
    }
}
