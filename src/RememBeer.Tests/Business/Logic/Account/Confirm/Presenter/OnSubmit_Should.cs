using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Account.Confirm;
using RememBeer.Business.Logic.Account.Confirm.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Account.Confirm.Presenter
{
    [TestFixture]
    public class OnSubmit_Should : TestClassBase
    {
        [Test]
        public void ChangeMessagesVisibility_WhenNotSuccessfull()
        {
            var mockedView = new Mock<IConfirmView>();
            var mockedArgs = new Mock<IConfirmEventArgs>();
            mockedArgs.Setup(a => a.UserId).Returns((string)null);
            mockedArgs.Setup(a => a.Code).Returns((string)null);
            var userService = new Mock<IUserService>();
            var presenter = new ConfirmPresenter(userService.Object, mockedView.Object);

            mockedView.Raise(x => x.OnSubmit += null, mockedView.Object, mockedArgs.Object);

            mockedView.VerifySet(v => v.SuccessPanelVisible = false, Times.Once);
            mockedView.VerifySet(v => v.ErrorPanelVisible = true, Times.Once);
        }

        [Test]
        public void ChangeMessagesVisibility_WhenUserIsNotConfirmed()
        {
            const string Email = "test@abv.bg";

            var mockedView = new Mock<IConfirmView>();
            var mockedArgs = MockedEventArgsGenerator.GetConfirmEventArgs();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.ConfirmEmail(mockedArgs.UserId, mockedArgs.Code))
                       .Returns(IdentityResult.Failed(Email));

            var presenter = new ConfirmPresenter(userService.Object, mockedView.Object);
            mockedView.Raise(x => x.OnSubmit += null, mockedView.Object, mockedArgs);

            mockedView.VerifySet(v => v.SuccessPanelVisible = false, Times.Once);
            mockedView.VerifySet(v => v.ErrorPanelVisible = true, Times.Once);
        }

        [Test]
        public void CallConfirmEmailMethod_WhenUserDataIsValid()
        {
            var mockedView = new Mock<IConfirmView>();
            var mockedArgs = MockedEventArgsGenerator.GetConfirmEventArgs();

            var userService = new Mock<IUserService>();
            userService.Setup(s => s.ConfirmEmail(mockedArgs.UserId, mockedArgs.Code))
                       .Returns(IdentityResult.Failed(new string[1]));

            var presenter = new ConfirmPresenter(userService.Object, mockedView.Object);

            mockedView.Raise(x => x.OnSubmit += null, mockedView.Object, mockedArgs);

            userService.Verify(f => f.ConfirmEmail(mockedArgs.UserId, mockedArgs.Code), Times.Once());
        }

        [Test]
        public void ChangeSuccessVisibility_WhenConfirmationSucceeded()
        {
            var mockedView = new Mock<IConfirmView>();
            var mockedArgs = MockedEventArgsGenerator.GetConfirmEventArgs();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.ConfirmEmail(mockedArgs.UserId, mockedArgs.Code))
                       .Returns(IdentityResult.Success);

            var presenter = new ConfirmPresenter(userService.Object, mockedView.Object);
            mockedView.Raise(x => x.OnSubmit += null, mockedView.Object, mockedArgs);

            mockedView.VerifySet(v => v.SuccessPanelVisible = true, Times.Once);
        }
    }
}
