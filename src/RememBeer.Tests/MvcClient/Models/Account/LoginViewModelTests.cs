using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Account;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Account
{
    [TestFixture]
    public class LoginViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expected = this.Fixture.Create<string>();
            var expectedpass = this.Fixture.Create<string>();
            var sut = new LoginViewModel();
            // Act
            sut.Email = expected;
            sut.Password = expectedpass;
            sut.RememberMe = true;
            // Assert
            Assert.AreEqual(true, sut.RememberMe);
            Assert.AreSame(expected, sut.Email);
            Assert.AreSame(expectedpass, sut.Password);
        }
    }
}
