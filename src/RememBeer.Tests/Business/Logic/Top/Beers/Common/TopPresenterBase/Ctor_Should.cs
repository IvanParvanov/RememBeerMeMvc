using System;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Beers;
using RememBeer.Business.Logic.Top.Common;

namespace RememBeer.Tests.Business.Logic.Top.Beers.Common.TopPresenterBase
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBeerServiceIsNull()
        {
            var mockedView = new Mock<IInitializableView<TopBeersViewModel>>();

            Assert.Throws<ArgumentNullException>(() => new TopPresenterBase<IInitializableView<TopBeersViewModel>>(null, mockedView.Object));
        }
    }
}
