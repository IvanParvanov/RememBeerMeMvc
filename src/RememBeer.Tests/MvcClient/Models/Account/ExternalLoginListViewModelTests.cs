using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Account;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Account
{
    [TestFixture]
    public class ExternalLoginListViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expected = this.Fixture.Create<string>();
            var sut = new ExternalLoginListViewModel();
            // Act
            sut.ReturnUrl = expected;
            // Assert
            Assert.AreSame(expected, sut.ReturnUrl);
        }
    }
}
