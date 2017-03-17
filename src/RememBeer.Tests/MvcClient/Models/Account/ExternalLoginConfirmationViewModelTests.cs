using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.AccountModels;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Account
{
    [TestFixture]
    public class ExternalLoginConfirmationViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedEmail = this.Fixture.Create<string>();
            var sut = new ExternalLoginConfirmationViewModel();
            // Act
            sut.Email = expectedEmail;
            // Assert
            Assert.AreSame(expectedEmail, sut.Email);
        }
    }
}
