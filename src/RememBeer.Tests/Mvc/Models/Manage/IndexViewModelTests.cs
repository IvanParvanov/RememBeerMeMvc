using System.Collections.Generic;

using Microsoft.AspNet.Identity;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Models.Manage
{
    [TestFixture]
    public class IndexViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedNumber = this.Fixture.Create<string>();
            var expectedLogins = new List<UserLoginInfo>();
            var sut = new IndexViewModel();
            // Act
            sut.BrowserRemembered = true;
            sut.HasPassword = true;
            sut.PhoneNumber = expectedNumber;
            sut.TwoFactor = true;
            sut.Logins = expectedLogins;
            // Assert
            Assert.AreSame(expectedNumber, sut.PhoneNumber);
            Assert.AreSame(expectedLogins, sut.Logins);
            Assert.AreEqual(true, sut.BrowserRemembered);
            Assert.AreEqual(true, sut.HasPassword);
            Assert.AreEqual(true, sut.TwoFactor);
        }
    }
}
