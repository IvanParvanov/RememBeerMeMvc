using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class ChangePassword_Should : TestClassBase
    {
        [Test]
        public void CallUserManagerChangepasswordMethodOnce()
        {
            var userId = this.Fixture.Create<string>();
            var currentPassword = this.Fixture.Create<string>();
            var newPassword = this.Fixture.Create<string>();
            var mockedUser = new MockedApplicationUser();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);
            userManager.Setup(m => m.ChangePassword(userId, currentPassword, newPassword))
                       .Returns(IdentityResult.Failed());

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.ChangePassword(userId, currentPassword, newPassword);

            userManager.Verify(m => m.ChangePassword(userId, currentPassword, newPassword), Times.Once);
        }

        [Test]
        public void CallFindByIdAndSignInMethodsOnce_IfResultIsSuccessfull()
        {
            var userId = this.Fixture.Create<string>();
            var currentPassword = this.Fixture.Create<string>();
            var newPassword = this.Fixture.Create<string>();
            var mockedUser = new MockedApplicationUser();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);
            userManager.Setup(m => m.ChangePassword(userId, currentPassword, newPassword))
                       .Returns(IdentityResult.Success);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.ChangePassword(userId, currentPassword, newPassword);

            userManager.Verify(m => m.FindById(userId), Times.Once);
            signInManager.Verify(m => m.SignIn(mockedUser, false, false), Times.Once);
        }

        [Test]
        public void CallReturnResult()
        {
            var userId = this.Fixture.Create<string>();
            var currentPassword = this.Fixture.Create<string>();
            var newPassword = this.Fixture.Create<string>();
            var mockedUser = new MockedApplicationUser();
            var expectedResult = IdentityResult.Success;

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);
            userManager.Setup(m => m.ChangePassword(userId, currentPassword, newPassword))
                       .Returns(expectedResult);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.ChangePassword(userId, currentPassword, newPassword);

            Assert.AreSame(expectedResult, result);
        }
    }
}
