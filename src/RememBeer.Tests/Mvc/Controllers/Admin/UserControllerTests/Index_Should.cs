using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;

namespace RememBeer.Tests.Mvc.Controllers.Admin.UserControllerTests
{
    [TestFixture]
    public class Index_Should : UsersControllerTestBase
    {
        [Test]
        public void Call_UserServicePaginatedUsersOnceWithDefaultParams_WhenNoParamsAreProvided()
        {
            // Arrange
            var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();

            // Act
            sut.Index();

            // Assert
            userService.Verify(s => s.PaginatedUsers(0, Constants.DefaultPageSize, ref anyInt, null), Times.Once);
        }

        [TestCase(0, 0, "")]
        [TestCase(1, 1, null)]
        [TestCase(37, 991, "sdakldasklj23891237123987123#$32456456s65asd56asd65asd5464234465234")]
        public void Call_UserServicePaginatedUsersOnceWithDefaultParams_WhenParamsAreProvided(int page, int pageSize, string searchPattern)
        {
            // Arrange
            var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();

            // Act
            sut.Index(page, pageSize, searchPattern);

            // Assert
            userService.Verify(s => s.PaginatedUsers(page, pageSize, ref anyInt, searchPattern), Times.Once);
        }

        [TestCase(-1)]
        [TestCase(-991)]
        [TestCase(-97)]
        public void Call_UserServicePaginatedUsersOnceWithAdjustedParams_WhenPageIsLessThanZero(int page)
        {
            // Arrange
            const int expectedPage = 0;
            var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();

            // Act
            sut.Index(page);

            // Assert
            userService.Verify(s => s.PaginatedUsers(expectedPage, It.IsAny<int>(), ref anyInt, It.IsAny<string>()), Times.Once);
        }

        [TestCase(-1)]
        [TestCase(-991)]
        [TestCase(-97)]
        public void Call_UserServicePaginatedUsersOnceWithAdjustedParams_WhenPageSizeIsLessThanZero(int pageSize)
        {
            // Arrange
            const int expectedPageSize = 1;
            var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();

            // Act
            sut.Index(pageSize: pageSize);

            // Assert
            userService.Verify(s => s.PaginatedUsers(It.IsAny<int>(), expectedPageSize, ref anyInt, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Return_CorrectPartialView_WhenRequestIsAjax()
        {
            // Arrange
            var expectedUsers = new List<IApplicationUser>();
            var expectedPage = 10;
            var expectedPageSize = 15;
            var httpContext = this.Kernel.Get<HttpContextBase>(AjaxContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();
            userService.Setup(s => s.PaginatedUsers(It.IsAny<int>(), It.IsAny<int>(), ref anyInt, It.IsAny<string>()))
                       .Returns(expectedUsers);

            // Act
            var result = sut.Index(expectedPage, expectedPageSize) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("_UserList", result.ViewName);

            var actual = result.Model as PaginatedViewModel<IApplicationUser>;
            Assert.IsNotNull(actual);
            Assert.AreSame(expectedUsers, actual.Items);
            Assert.AreEqual(expectedPage, actual.CurrentPage);
            Assert.AreEqual(0, actual.TotalCount);
            Assert.AreEqual(expectedPageSize, actual.PageSize);
        }

        [Test]
        public void Return_CorrectView_WhenRequestIsNotAjax()
        {
            // Arrange
            var expectedUsers = new List<IApplicationUser>();
            var expectedPage = 10;
            var expectedPageSize = 15;
            var httpContext = this.Kernel.Get<HttpContextBase>(RegularContextName);
            var sut = this.Kernel.Get<UsersController>();
            sut.ControllerContext = new ControllerContext(httpContext, new RouteData(), sut);
            var userService = this.Kernel.GetMock<IUserService>();
            var anyInt = It.IsAny<int>();
            userService.Setup(s => s.PaginatedUsers(It.IsAny<int>(), It.IsAny<int>(), ref anyInt, It.IsAny<string>()))
                       .Returns(expectedUsers);

            // Act
            var result = sut.Index(expectedPage, expectedPageSize) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains("Index", result.ViewName);

            var actual = result.Model as PaginatedViewModel<IApplicationUser>;
            Assert.IsNotNull(actual);
            Assert.AreSame(expectedUsers, actual.Items);
            Assert.AreEqual(expectedPage, actual.CurrentPage);
            Assert.AreEqual(0, actual.TotalCount);
            Assert.AreEqual(expectedPageSize, actual.PageSize);
        }
    }
}
