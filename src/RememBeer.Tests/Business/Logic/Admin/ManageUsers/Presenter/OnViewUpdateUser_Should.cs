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
    public class OnViewUpdateUser_Should : TestClassBase
    {
        [Test]
        public void Call_UserServiceUpdateUserMethodOnceWithCorrectParams()
        {
            var args = MockedEventArgsGenerator.GetMockedUserUpdateEventArgs();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.UpdateUser(args.Id, args.Email, args.UserName, args.IsConfirmed))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserUpdate += null, view.Object, args);

            userService.Verify(s => s.UpdateUser(args.Id, args.Email, args.UserName, args.IsConfirmed), Times.Once);
        }

        [Test]
        public void Set_ViewSuccessMessage_WhenResultSucceeds()
        {
            var args = MockedEventArgsGenerator.GetMockedUserUpdateEventArgs();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.UpdateUser(args.Id, args.Email, args.UserName, args.IsConfirmed))
                       .Returns(IdentityResult.Success);

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserUpdate += null, view.Object, args);

            view.VerifySet(v => v.SuccessMessageVisible = true);
            view.VerifySet(v => v.SuccessMessageText = It.IsAny<string>());
        }

        [Test]
        public void Set_ViewErrorMessage_WhenResultFails()
        {
            var args = MockedEventArgsGenerator.GetMockedUserUpdateEventArgs();
            var view = new Mock<IManageUsersView>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.UpdateUser(args.Id, args.Email, args.UserName, args.IsConfirmed))
                       .Returns(IdentityResult.Failed());

            var sut = new ManageUsersPresenter(userService.Object, view.Object);
            view.Raise(v => v.UserUpdate += null, view.Object, args);

            view.VerifySet(v => v.ErrorMessageVisible = true);
            view.VerifySet(v => v.ErrorMessageText = It.IsAny<string>());
        }
    }
}
