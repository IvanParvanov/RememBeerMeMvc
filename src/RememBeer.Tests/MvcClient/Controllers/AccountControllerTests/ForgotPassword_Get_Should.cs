using System.Web.Mvc;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class ForgotPassword_Get_Should : AccountControllerTestBase
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ForgotPassword(), typeof(AllowAnonymousAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Return_View()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var result = sut.ForgotPassword();

            // Assert
            Assert.AreEqual("", result.ViewName);
        }
    }
}
