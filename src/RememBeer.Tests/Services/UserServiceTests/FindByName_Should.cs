using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class FindByName_Should : TestClassBase
    {
        [Test]
        public void CallFindByNameMethodOnce()
        {
            // Arrange
            var userName = this.Fixture.Create<string>();
            var mockedUser = new MockedApplicationUser();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByName(userName))
                       .Returns(mockedUser);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.FindByName(userName);

            // Assert
            userManager.Verify(m => m.FindByName(userName), Times.Once);
        }

        [Test]
        public void ReturnValueFromUserManager_WhenUserIsFound()
        {
            // Arrange
            var userName = this.Fixture.Create<string>();

            var mockedUser = new MockedApplicationUser();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByName(userName))
                       .Returns(mockedUser);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.FindByName(userName);

            // Assert
            Assert.AreSame(mockedUser, result);
        }

        [Test]
        public void ReturnValueFromUserManager_WhenUserIsNotFound()
        {
            // Arrange
            var userName = this.Fixture.Create<string>();
            var expectedResult = (ApplicationUser)null;
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.FindByName(userName))
                       .Returns(expectedResult);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.FindByName(userName);

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
