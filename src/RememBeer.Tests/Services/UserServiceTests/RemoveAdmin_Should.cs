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
    internal class RemoveAdmin_Should : TestClassBase
    {
        [Test]
        public void Call_UserManagerAddToRoleAsyncMethodOnceWithCorrectParams()
        {
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.RemoveFromRoleAsync(expectedId, Constants.AdminRole))
                       .Returns(Task.FromResult(IdentityResult.Failed()));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.RemoveAdmin(expectedId);

            userManager.Verify(m => m.RemoveFromRoleAsync(expectedId, Constants.AdminRole), Times.Once);
        }

        [Test]
        public void ReturnResultFrom_UserManagerAddToRoleAsyncMethod()
        {
            var expectedResult = IdentityResult.Failed();
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.RemoveFromRoleAsync(expectedId, Constants.AdminRole))
                       .Returns(Task.FromResult(expectedResult));

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.RemoveAdmin(expectedId);

            Assert.AreSame(expectedResult, result);
        }
    }
}
