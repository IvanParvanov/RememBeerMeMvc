using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class EnableUserAsync_Should : TestClassBase
    {
        [Test]
        public async Task CallUserManagerSetLockoutEndDateAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.SetLockoutEndDateAsync(expectedId, DateTimeOffset.MinValue))
                       .Returns(Task.FromResult(IdentityResult.Success));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = await service.EnableUserAsync(expectedId);

            // Assert
            userManager.Verify(m => m.SetLockoutEndDateAsync(expectedId, DateTimeOffset.MinValue), Times.Once);
        }

        [Test]
        public async Task ReturnResultFrom_UserManagerSetLockoutEndDateAsyncMethod()
        {
            // Arrange
            var expectedResult = IdentityResult.Success;
            var id = this.Fixture.Create<string>();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.SetLockoutEndDateAsync(It.IsAny<string>(), It.IsAny<DateTimeOffset>()))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = await service.EnableUserAsync(id);

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
