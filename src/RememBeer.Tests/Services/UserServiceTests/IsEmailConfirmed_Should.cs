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
    public class IsEmailConfirmed_Should : TestClassBase
    {
        [Test]
        public void CallFindByNameMethodOnce()
        {
            var userId = this.Fixture.Create<string>();
            var expectedResult = this.Fixture.Create<bool>();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.IsEmailConfirmed(userId))
                       .Returns(expectedResult);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.IsEmailConfirmed(userId);

            userManager.Verify(m => m.IsEmailConfirmed(userId), Times.Once);
        }

        [Test]
        public void ReturnValueFromUserManager_WhenUserIsFound()
        {
            var userId = this.Fixture.Create<string>();
            var expectedResult = this.Fixture.Create<bool>();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.IsEmailConfirmed(userId))
                       .Returns(expectedResult);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.IsEmailConfirmed(userId);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
