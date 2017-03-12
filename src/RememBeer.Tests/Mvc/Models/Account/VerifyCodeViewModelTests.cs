using System.Collections.Generic;
using System.Web.Mvc;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.AccountModels;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Models.Account
{
    [TestFixture]
    public class VerifyCodeViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedUrl = this.Fixture.Create<string>();
            var expectedProvider = this.Fixture.Create<string>();
            var expectedCode = this.Fixture.Create<string>();
            var sut = new VerifyCodeViewModel();
            // Act
            sut.ReturnUrl = expectedUrl;
            sut.RememberMe = true;
            sut.Provider = expectedProvider;
            sut.RememberBrowser = true;
            sut.Code = expectedCode;
            // Assert
            Assert.AreSame(expectedUrl, sut.ReturnUrl);
            Assert.AreSame(expectedProvider, sut.Provider);
            Assert.AreSame(expectedCode, sut.Code);
            Assert.AreEqual(true, sut.RememberMe);
            Assert.AreEqual(true, sut.RememberBrowser);
        }
    }
}
