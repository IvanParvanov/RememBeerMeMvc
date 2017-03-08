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
    public class OnViewEnableUser_Should
    {
        [Test]
        public void Call_UserServiceEnableUserMethodOnceWithCorrectParams()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.EnableUser(args.Id))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserEnable += null, view.Object, args);

            userService.Verify(s => s.EnableUser(args.Id), Times.Once);
        }

        [Test]
        public void Set_ViewSuccessMessage_WhenResultSucceeds()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.EnableUser(args.Id))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserEnable += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageVisible = true);
            view.VerifySet(v => v.SuccessMessageText = It.IsAny<string>());
        }

        [Test]
        public void Set_ViewErrorMessage_WhenResultFails()
        {
            var args = MockedEventArgsGenerator.GetIdentifiableEventArgs<string>();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.EnableUser(args.Id))
                       .Returns(IdentityResult.Failed());

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserEnable += null, view.Object, args);

            view.VerifySet(v => v.ErrorMessageVisible = true);
            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>());
        }
    }
}
