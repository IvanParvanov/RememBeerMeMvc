using System.Threading.Tasks;

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
    public class UpdateUser_Should : TestClassBase
    {
        [Test]
        public void Call_UserManagerFindByIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var userId = this.Fixture.Create<string>();
            var email = this.Fixture.Create<string>();
            var username = this.Fixture.Create<string>();
            var isConfirmed = this.Fixture.Create<bool>();
            var mockedUser = new MockedApplicationUser();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            service.UpdateUser(userId, email, username, isConfirmed);

            // Assert
            userManager.Verify(m => m.FindById(userId), Times.Once);
        }

        [Test]
        public void CallAndReturnResultFromUserManagerUpdateAsyncMethod()
        {
            // Arrange
            var expectedResult = IdentityResult.Failed();
            var userId = this.Fixture.Create<string>();
            var email = this.Fixture.Create<string>();
            var username = this.Fixture.Create<string>();
            var isConfirmed = this.Fixture.Create<bool>();
            var mockedUser = new MockedApplicationUser();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);
            userManager.Setup(m => m.UpdateAsync(mockedUser))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.UpdateUser(userId, email, username, isConfirmed);

            // Assert
            Assert.AreSame(expectedResult, result);
            userManager.Verify(m => m.UpdateAsync(mockedUser), Times.Once);
        }

        [Test]
        public void SetUserPropertiesCorrectly()
        {
            // Arrange
            var expectedResult = IdentityResult.Failed();
            var userId = this.Fixture.Create<string>();
            var email = this.Fixture.Create<string>();
            var username = this.Fixture.Create<string>();
            var isConfirmed = this.Fixture.Create<bool>();
            var mockedUser = new MockedApplicationUser()
                             {
                                 EmailConfirmed = !isConfirmed
                             };

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(f => f.FindById(userId))
                       .Returns(mockedUser);
            userManager.Setup(m => m.UpdateAsync(mockedUser))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            service.UpdateUser(userId, email, username, isConfirmed);

            // Assert
            Assert.AreSame(email, mockedUser.Email);
            Assert.AreSame(username, mockedUser.UserName);
            Assert.AreEqual(isConfirmed, mockedUser.EmailConfirmed);
        }
    }
}
