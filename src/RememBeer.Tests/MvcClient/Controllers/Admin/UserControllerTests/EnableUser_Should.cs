using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Areas.Admin.Controllers;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.Admin.UserControllerTests
{
    [TestFixture]
    public class EnableUser_Should : UsersControllerTestBase
    {
        [TestCase("")]
        [TestCase(null)]
        [TestCase("user001sdkjasdjklasd789@#&*(23789123789sd")]
        public async Task Call_UserServiceEnableUserAsyncOnceWithCorrectParams(string userId)
        {
            // Arrange
            var sut = this.MockingKernel.Get<UsersController>();
            var userService = this.MockingKernel.GetMock<IUserService>();

            // Act
            await sut.EnableUser(userId);

            // Assert
            userService.Verify(s => s.EnableUserAsync(userId), Times.Once);
        }

        [Test]
        public async Task Return_CorrectRedirectToActionResult()
        {
            // Arrange
            var expectedPageSize = 17;
            var expectedAction = "Index";
            var expectedPage = 991;
            var expectedSearch = Guid.NewGuid().ToString();
            var sut = this.MockingKernel.Get<UsersController>();

            // Act
            var result = await sut.EnableUser("sa", expectedPage, expectedPageSize, expectedSearch) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual((string)result.RouteValues["action"], expectedAction);
            Assert.AreEqual((int)result.RouteValues["page"], expectedPage);
            Assert.AreEqual((int)result.RouteValues["pageSize"], expectedPageSize);
            Assert.AreEqual((string)result.RouteValues["searchPattern"], expectedSearch);
        }
    }
}
