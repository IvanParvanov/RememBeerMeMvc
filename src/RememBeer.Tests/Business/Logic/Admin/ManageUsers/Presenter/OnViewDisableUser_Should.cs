using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Admin.ManageUsers;
using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.ManageUsers.Presenter
{
    [TestFixture]
    public class OnViewDisableUser_Should
    {
        [Test]
        public void Call_UserServiceDisableUserMethodOnceWithCorrectParams()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.DisableUser(args.Id))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserDisable += null, view.Object, args);

            userService.Verify(s => s.DisableUser(args.Id), Times.Once);
        }

        [Test]
        public void Set_ViewSuccessMessage_WhenResultSucceeds()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.DisableUser(args.Id))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserDisable += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageVisible = true);
            view.VerifySet(v => v.SuccessMessageText = It.IsAny<string>());
        }

        [Test]
        public void Set_ViewErrorMessage_WhenResultFails()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.DisableUser(args.Id))
                       .Returns(IdentityResult.Failed());

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserDisable += null, view.Object, args);

            view.VerifySet(v => v.ErrorMessageVisible = true);
            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>());
        }

    }
}
