using System.Collections.Generic;

using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Account.ManagePassword;
using RememBeer.Business.Logic.Account.ManagePassword.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Account.ManagePassword.Presenter
{
    [TestFixture]
    public class OnChangePassword_Should : TestClassBase
    {
        [Test]
        public void CallUserServiceChangePasswordMethod_WithCorrectParameters()
        {
            var expectedId = this.Fixture.Create<string>();
            var expectedOld = this.Fixture.Create<string>();
            var expectedNew = this.Fixture.Create<string>();

            var view = new Mock<IManagePasswordView>();
            var args = new Mock<IChangePasswordEventArgs>();
            args.Setup(a => a.UserId).Returns(expectedId);
            args.Setup(a => a.CurrentPassword).Returns(expectedOld);
            args.Setup(a => a.NewPassword).Returns(expectedNew);

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.ChangePassword(expectedId, expectedOld, expectedNew))
                       .Returns(IdentityResult.Success);

            var httpResponse = new MockedHttpResponse();
            var presenter = new ManagePasswordPresenter(userService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.ChangePassword += null, view.Object, args.Object);

            userService.Verify(s => s.ChangePassword(expectedId, expectedOld, expectedNew), Times.Once());
        }

        [Test]
        public void CallAddErrors_WhenResultHasFailed()
        {
            var view = new Mock<IManagePasswordView>();
            var userService = new Mock<IUserService>();
            userService.Setup(m => m.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(IdentityResult.Failed("error"));

            var args = new Mock<IChangePasswordEventArgs>();

            var presenter = new ManagePasswordPresenter(userService.Object, view.Object);
            view.Raise(v => v.ChangePassword += null, view.Object, args.Object);

            view.Verify(v => v.AddErrors(It.IsAny<IList<string>>()));
        }

        [Test]
        public void RedirectToCorrectUrl_WhenResultSucceeds()
        {
            var view = new Mock<IManagePasswordView>();
            var userService = new Mock<IUserService>();
            var args = new Mock<IChangePasswordEventArgs>();
            userService.Setup(s => s.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(IdentityResult.Success);

            var httpResponse = new MockedHttpResponse();

            var presenter = new ManagePasswordPresenter(userService.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };
            view.Raise(v => v.ChangePassword += null, view.Object, args.Object);

            Assert.AreEqual("~/Account/Manage?m=ChangePwdSuccess", httpResponse.RedirectUrl);
        }
    }
}
