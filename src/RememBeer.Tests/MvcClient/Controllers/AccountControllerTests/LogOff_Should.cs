using System;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class LogOff_Should : AccountControllerTestBase
    {
        [TestCase(typeof(HttpPostAttribute))]
        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.LogOff(), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Call_AuthenticationManagerSignOutMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();
            var auth = this.MockingKernel.GetMock<IAuthenticationManager>();

            // Act
            sut.LogOff();

            // Assert
            auth.Verify(a => a.SignOut(DefaultAuthenticationTypes.ApplicationCookie));
        }

        [Test]
        public void Return_CorrectRedirect()
        {
            // Arrange
            var sut = this.MockingKernel.Get<AccountController>();

            // Act
            var result = sut.LogOff();

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }
    }
}
