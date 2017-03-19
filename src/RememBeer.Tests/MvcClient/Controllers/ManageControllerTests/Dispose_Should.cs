using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    [TestFixture]
    public class Dispose_Should : ManageControllerTestBase
    {
        [Test]
        public void DoNothing_WhenDisposingIsFalse()
        {
            // Arrange
            var sut = this.MockingKernel.Get<MockedManageController>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            sut.Dispose(false);

            // Assert
            userManager.Verify(m => m.Dispose(), Times.Never);
        }

        [Test]
        public void Call_UserManagerDisposeMethodOnce_WhenDisposingIsTrue()
        {
            // Arrange
            var sut = this.MockingKernel.Get<MockedManageController>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            sut.Dispose(true);

            // Assert
            userManager.Verify(m => m.Dispose(), Times.Once);
        }
    }
}
