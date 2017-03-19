using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Account;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Account
{
    [TestFixture]
    public class ResetPasswordViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expected = this.Fixture.Create<string>();
            var expectedpass = this.Fixture.Create<string>();
            var expectedCode = this.Fixture.Create<string>();
            var sut = new ResetPasswordViewModel();
            // Act
            sut.Email = expected;
            sut.Password = expectedpass;
            sut.ConfirmPassword = expectedpass;
            sut.Code = expectedCode;
            // Assert
            Assert.AreSame(expected, sut.Email);
            Assert.AreSame(expectedpass, sut.Password);
            Assert.AreSame(expectedpass, sut.ConfirmPassword);
            Assert.AreSame(expectedCode, sut.Code);
        }
    }
}
