using System;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Account.Common.Presenters;
using RememBeer.Business.Logic.Account.Confirm.Contracts;

namespace RememBeer.Tests.Business.Logic.Account.Common
{
    [TestFixture]
    public class UserServicePresenterTests
    {
        [Test]
        public void Ctor_ShouldThrowArgumentNullException_WhenArgumentsAreNull()
        {
            var mockedView = new Mock<IConfirmView>();
            Assert.Throws<ArgumentNullException>(() => new UserServicePresenter<IConfirmView>(null, mockedView.Object));
        }
    }
}
