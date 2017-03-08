using NUnit.Framework;

using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.ManageUsers.Presenter
{
    [TestFixture]
    public class OnViewUserSearch_Should : TestClassBase
    {
        //TODO: Figure out out in Moq
        //[Test]
        //public void Call_UserServicePaginatedUsersMethodOnceWithCorrectParams()
        //{
        //    var pattern = this.Fixture.Create<string>();
        //    var currentPage = this.Fixture.Create<int>();
        //    var pagesize = this.Fixture.Create<int>();

        //    var view = new Mock<IManageUsersView>();
        //    view.Setup(v => v.CurrentPage)
        //        .Returns(currentPage);
        //    view.Setup(v => v.PageSize)
        //        .Returns(pagesize);

        //    var userService = new Mock<IUserService>();

        //    var sut = new ManageUsersPresenter(userService.Object, view.Object);

        //    var args = new Mock<ISearchEventArgs>();
        //    args.Setup(a => a.Pattern)
        //        .Returns(pattern);

        //    view.Raise(v => v.UserSearch += null, view.Object, args.Object);

        //    userService.Verify(s => s.PaginatedUsers(currentPage, pagesize, out It.IsAny<int>(), pattern), Times.Once);
        //}

        //TODO: Figure out out in Moq
        //[Test]
        //public void SetViewPropertiesCorrectly()
        //{
        //    var pattern = this.Fixture.Create<string>();
        //    var currentPage = this.Fixture.Create<int>();
        //    var pagesize = this.Fixture.Create<int>();
        //    var expectedUsers = new List<IApplicationUser>();

        //    var viewModel = new MockedManageUsersViewModel();
        //    var view = new Mock<IManageUsersView>();
        //    view.Setup(v => v.CurrentPage)
        //        .Returns(currentPage);
        //    view.Setup(v => v.PageSize)
        //        .Returns(pagesize);
        //    view.Setup(v => v.Model)
        //        .Returns(viewModel);

        //    var userService = new Mock<IUserService>();
        //    userService.Setup(s =>
        //                          s.PaginatedUsers(It.IsAny<int>(), It.IsAny<int>(), out It.IsAny<int>(), It.IsAny<string>()))
        //               .Returns(expectedUsers);

        //    var sut = new ManageUsersPresenter(userService.Object, view.Object);

        //    var args = new Mock<ISearchEventArgs>();
        //    args.Setup(a => a.Pattern)
        //        .Returns(pattern);

        //    view.Raise(v => v.UserSearch += null, view.Object, args.Object);

        //    Assert.AreSame(expectedUsers, viewModel.Users);
        //}
    }
}
