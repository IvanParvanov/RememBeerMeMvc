using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.Tests.MvcClient.Controllers.AccountControllerTests.Mocks;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Dispose_Should : AccountControllerTestBase
    {
        public override void Init()
        {
            base.Init();
            this.MockingKernel.Bind<MockedAccountController>().ToSelf();
        }

        [Test]
        public void DoNothing_WhenDisposingIsFalse()
        {
            // Arrange
            var sut = this.MockingKernel.Get<MockedAccountController>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();
            var signInManager = this.MockingKernel.GetMock<IApplicationSignInManager>();

            // Act
            sut.Dispose(false);

            // Assert
            userManager.Verify(m => m.Dispose(), Times.Never);
            signInManager.Verify(m => m.Dispose(), Times.Never);
        }

        [Test]
        public void Call_UserManagerDisposeMethodOnce_WhenDisposingIsTrue()
        {
            // Arrange
            var sut = this.MockingKernel.Get<MockedAccountController>();
            var userManager = this.MockingKernel.GetMock<IApplicationUserManager>();

            // Act
            sut.Dispose(true);

            // Assert
            userManager.Verify(m => m.Dispose(), Times.Once);
        }

        [Test]
        public void Call_SignInManagerDisposeMethodOnce_WhenDisposingIsTrue()
        {
            // Arrange
            var sut = this.MockingKernel.Get<MockedAccountController>();
            var signInManager = this.MockingKernel.GetMock<IApplicationSignInManager>();

            // Act
            sut.Dispose(true);

            // Assert
            signInManager.Verify(m => m.Dispose(), Times.Once);
        }
    }
}
