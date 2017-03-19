using System.Web.Mvc;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Login_Get_Should : AccountControllerTestBase
    {
        [Test]
        public void HaveAllowAnonymousAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Login(default(string)), typeof(AllowAnonymousAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(null)]
        [TestCase("http://localhost:54656")]
        [TestCase("klasdjkljasdjklasdjklas")]
        public void ReturnCorrectView(string returnUrl)
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var result = sut.Login(returnUrl);

            // Assert
            Assert.AreSame(returnUrl, result.ViewBag.ReturnUrl);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
