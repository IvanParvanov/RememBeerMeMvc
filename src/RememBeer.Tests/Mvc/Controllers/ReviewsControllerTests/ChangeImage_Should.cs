using System.Web.Mvc;

using NUnit.Framework;

using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.Tests.Mvc.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class ChangeImage_Should : ReviewsControllerTestBase
    {
        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(typeof(ReviewsController), nameof(ReviewsController.ChangeImage), typeof(ValidateAntiForgeryTokenAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(typeof(ReviewsController), nameof(ReviewsController.ChangeImage), typeof(AjaxOnlyAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveHttpPostAttribute()
        {
            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(typeof(ReviewsController), nameof(ReviewsController.ChangeImage), typeof(HttpPostAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
