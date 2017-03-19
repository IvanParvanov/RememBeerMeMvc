using System.Web.Mvc;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class ResetPassword_Get_Should : AccountControllerTestBase
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ResetPassword(default(string)), typeof(AllowAnonymousAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Return_ErrorView_WhenCodeIsNull()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var result = sut.ResetPassword((string)null);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void Return_View_WhenCodeIsNotNull()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var result = sut.ResetPassword("notnull");

            // Assert
            Assert.AreEqual("", result.ViewName);
        }
    }
}
