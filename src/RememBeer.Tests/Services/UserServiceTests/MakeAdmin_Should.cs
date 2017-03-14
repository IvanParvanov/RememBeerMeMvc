using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    internal class MakeAdmin_Should : TestClassBase
    {
        [Test]
        public async Task Call_UserManagerAddToRoleAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.AddToRoleAsync(expectedId, Constants.AdminRole))
                       .Returns(Task.FromResult(IdentityResult.Failed()));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = await service.MakeAdminAsync(expectedId);

            // Assert
            userManager.Verify(m => m.AddToRoleAsync(expectedId, Constants.AdminRole), Times.Once);
        }

        [Test]
        public async Task ReturnResultFrom_UserManagerAddToRoleAsyncMethod()
        {
            // Arrange
            var expectedResult = IdentityResult.Failed();
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.AddToRoleAsync(expectedId, Constants.AdminRole))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = await service.MakeAdminAsync(expectedId);

            // Assert
            Assert.AreSame(expectedResult, result);
        }
    }
}
