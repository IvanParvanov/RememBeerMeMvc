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
    public class DisableUser_Should : TestClassBase
    {
        [Test]
        public void CallUserManagerUpdateSecurityStampAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.UpdateSecurityStampAsync(expectedId))
                       .Returns(Task.FromResult(IdentityResult.Failed()));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.DisableUser(expectedId);

            // Assert
            userManager.Verify(m => m.UpdateSecurityStampAsync(expectedId), Times.Once);
        }

        [Test]
        public void ReturnResultFrom_UserManagerUpdateSecurityStampAsyncMethod_WhenItReturnsFail()
        {
            // Arrange
            var expectedResult = IdentityResult.Failed();
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.UpdateSecurityStampAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.DisableUser(expectedId);

            // Assert
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Call_UserManagerSetLockoutEndDateAsyncMethodOnceWithCorrectparams_WhenTimeStampChangeIsSuccessfull()
        {
            // Arrange
            var expectedResult = IdentityResult.Success;
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.UpdateSecurityStampAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.DisableUser(expectedId);

            userManager.Verify(m => m.SetLockoutEndDateAsync(expectedId, DateTimeOffset.MaxValue), Times.Once);
        }

        [Test]
        public void
            ReturnResultFrom_UserManagerSetLockoutEndDateAsyncMethodOnceWithCorrectparams_WhenTimeStampChangeIsSuccessfull()
        {
            // Arrange
            var expectedResult = IdentityResult.Success;
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.UpdateSecurityStampAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(expectedResult));
            userManager.Setup(m => m.SetLockoutEndDateAsync(It.IsAny<string>(), It.IsAny<DateTimeOffset>()))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.DisableUser(expectedId);

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
