using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Manage
{
    [TestFixture]
    public class SetPasswordModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedPass = this.Fixture.Create<string>();
            var expectedConfirm = this.Fixture.Create<string>();
            var sut = new SetPasswordViewModel();
            // Act
            sut.NewPassword = expectedPass;
            sut.ConfirmPassword = expectedConfirm;
            // Assert
            Assert.AreSame(expectedPass, sut.NewPassword);
            Assert.AreSame(expectedConfirm, sut.ConfirmPassword);
        }
    }
}
