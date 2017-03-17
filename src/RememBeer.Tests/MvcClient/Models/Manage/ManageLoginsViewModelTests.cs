using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using NUnit.Framework;

using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Manage
{
    [TestFixture]
    public class ManageLoginsModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedOther = new List<AuthenticationDescription>();
            var expectedLogins = new List<UserLoginInfo>();
            var sut = new ManageLoginsViewModel();
            // Act
            sut.CurrentLogins = expectedLogins;
            sut.OtherLogins = expectedOther;
            // Assert
            Assert.AreSame(expectedLogins, sut.CurrentLogins);
            Assert.AreSame(expectedOther, sut.OtherLogins);
        }
    }
}
