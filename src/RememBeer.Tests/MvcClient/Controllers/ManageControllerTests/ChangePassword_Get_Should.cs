using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.Tests.MvcClient.Controllers.Ninject;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests
{
    public class ChangePassword_Get_Should : ManageControllerTestBase
    {
        [Test]
        public void ReturnCorrectView()
        {
            // Arrange
            var sut = this.Kernel.Get<ManageController>();

            // Act
            var result = sut.ChangePassword();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}
