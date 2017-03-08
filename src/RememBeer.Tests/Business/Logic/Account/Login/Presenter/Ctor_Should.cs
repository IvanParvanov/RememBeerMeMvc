using System;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Account.Login;
using RememBeer.Business.Logic.Account.Login.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.Business.Logic.Account.Login.Presenter
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenHelperIsNull()
        {
            var mockedView = new Mock<ILoginView>();
            var userService = new Mock<IUserService>();

            Assert.Throws<ArgumentNullException>(() => new LoginPresenter(userService.Object, null, mockedView.Object));
        }
    }
}
