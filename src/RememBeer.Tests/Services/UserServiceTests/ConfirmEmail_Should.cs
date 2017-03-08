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
    public class ConfirmEmail_Should : TestClassBase
    {
        [Test]
        public void CallConfirmEmailMethodExactlyOnce()
        {
            var userId = this.Fixture.Create<string>();
            var code = this.Fixture.Create<string>();

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.ConfirmEmail(userId, code))
                       .Returns(IdentityResult.Success);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.ConfirmEmail(userId, code);

            userManager.Verify(m => m.ConfirmEmail(userId, code), Times.Once);
        }

        [Test]
        public void ReturnResult()
        {
            var userId = this.Fixture.Create<string>();
            var code = this.Fixture.Create<string>();
            var expectedResult = IdentityResult.Success;

            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.ConfirmEmail(userId, code))
                       .Returns(expectedResult);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            var result = service.ConfirmEmail(userId, code);

            Assert.AreSame(expectedResult, result);
        }
    }
}
