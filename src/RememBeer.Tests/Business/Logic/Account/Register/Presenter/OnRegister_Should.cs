using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Account.Register;
using RememBeer.Business.Logic.Account.Register.Contracts;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Account.Register.Presenter
{
    public class OnRegister_Should : TestClassBase
    {
        [Test]
        public void CallUserServiceRegisterUserMethod_WithCorrectParameters()
        {
            var view = new Mock<IRegisterView>();
            var identityHelper = new Mock<IIdentityHelper>();
            var args = this.GetMockedEventArgs();

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.RegisterUser(args.UserName, args.Email, args.Password))
                       .Returns(IdentityResult.Failed(new string[1]));

            var httpResponse = new MockedHttpResponse();
            var presenter = new RegisterPresenter(userService.Object, identityHelper.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.OnRegister += null, view.Object, args);

            userService.Verify(s => s.RegisterUser(args.UserName, args.Email, args.Password), Times.Once());
        }

        [Test]
        public void SetViewErrors_WhenRegisterFails()
        {
            var expectedMessage = this.Fixture.Create<string>();
            var view = new Mock<IRegisterView>();
            var identityHelper = new Mock<IIdentityHelper>();
            var args = this.GetMockedEventArgs();

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.RegisterUser(args.UserName, args.Email, args.Password))
                       .Returns(IdentityResult.Failed(new[] { expectedMessage }));

            var httpResponse = new MockedHttpResponse();
            var presenter = new RegisterPresenter(userService.Object, identityHelper.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };

            view.Raise(v => v.OnRegister += null, view.Object, args);

            view.VerifySet(v => v.ErrorMessageText = expectedMessage, Times.Once());
        }

        [Test]
        public void CallGetReturnUrl_WhenRegisterSucceeds()
        {
            var returnUrl = this.Fixture.Create<string>();
            const string returnUrlKey = "ReturnUrl";

            var view = new Mock<IRegisterView>();
            var args = this.GetMockedEventArgs();

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.RegisterUser(args.UserName, args.Email, args.Password))
                       .Returns(IdentityResult.Success);

            var identityHelper = new Mock<IIdentityHelper>();
            identityHelper.Setup(i => i.GetReturnUrl(returnUrl))
                          .Returns(returnUrl);

            var httpResponse = new MockedHttpResponse();
            var presenter = new RegisterPresenter(userService.Object, identityHelper.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };
            presenter.HttpContext.Request.QueryString.Add(returnUrlKey, returnUrl);

            view.Raise(v => v.OnRegister += null, view.Object, args);

            identityHelper.Verify(i => i.GetReturnUrl(returnUrl), Times.Once());
        }

        [Test]
        public void RedirectToCorrectPage_WhenRegisterSucceeds()
        {
            var query = this.Fixture.Create<string>();
            var returnUrl = this.Fixture.Create<string>();
            const string returnUrlKey = "ReturnUrl";

            var view = new Mock<IRegisterView>();
            var args = this.GetMockedEventArgs();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.RegisterUser(args.UserName, args.Email, args.Password))
                       .Returns(IdentityResult.Success);

            var identityHelper = new Mock<IIdentityHelper>();
            identityHelper.Setup(i => i.GetReturnUrl(query))
                          .Returns(returnUrl);

            var httpResponse = new MockedHttpResponse();
            var presenter = new RegisterPresenter(userService.Object, identityHelper.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(httpResponse)
                            };
            presenter.HttpContext.Request.QueryString.Add(returnUrlKey, query);

            view.Raise(v => v.OnRegister += null, view.Object, args);

            Assert.AreEqual(returnUrl, httpResponse.RedirectUrl);
        }

        private IRegisterEventArgs GetMockedEventArgs()
        {
            var expectedEmail = this.Fixture.Create<string>();
            var expectedPassword = this.Fixture.Create<string>();
            var expectedName = this.Fixture.Create<string>();
            var args = new Mock<IRegisterEventArgs>();
            args.Setup(a => a.Email).Returns(expectedEmail);
            args.Setup(a => a.Password).Returns(expectedPassword);
            args.Setup(a => a.UserName).Returns(expectedName);

            return args.Object;
        }
    }
}
