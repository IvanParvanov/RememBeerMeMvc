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
    public class GetById_Should : TestClassBase
    {
        [Test]
        public void CallAndReturnValueFromFindByIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var expectedUser = new MockedApplicationUser();
            var expectedId = this.Fixture.Create<string>();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(m => m.FindById(expectedId))
                       .Returns(expectedUser);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();

            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);

            // Act
            var result = service.GetById(expectedId);

            // Assert
            Assert.AreSame(expectedUser, result);
            userManager.Verify(u => u.FindById(expectedId), Times.Once);
        }
    }
}
