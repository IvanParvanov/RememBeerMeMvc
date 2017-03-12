using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Models.Manage
{
    [TestFixture]
    public class ChangePasswordViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedOld = this.Fixture.Create<string>();
            var expectedConfirm = this.Fixture.Create<string>();
            var expectedNew = this.Fixture.Create<string>();
            var sut = new ChangePasswordViewModel();
            // Act
            sut.ConfirmPassword = expectedConfirm;
            sut.OldPassword = expectedOld;
            sut.NewPassword = expectedNew;
            // Assert
            Assert.AreSame(expectedConfirm, sut.ConfirmPassword);
            Assert.AreSame(expectedOld, sut.OldPassword);
            Assert.AreSame(expectedNew, sut.NewPassword);
        }
    }
}
