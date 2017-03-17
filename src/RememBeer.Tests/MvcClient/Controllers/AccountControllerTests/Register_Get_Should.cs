using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests
{
    [TestFixture]
    public class Register_Get_Should : AccountControllerTestBase
    {
        [TestCase(typeof(AllowAnonymousAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Register(), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void ReturnCorrectView()
        {
            // Arrange
            var sut = this.Kernel.Get<AccountController>();

            // Act
            var result = sut.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
