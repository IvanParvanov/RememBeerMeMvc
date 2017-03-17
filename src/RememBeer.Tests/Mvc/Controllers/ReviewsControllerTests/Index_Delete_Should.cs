using System;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Index_Delete_Should : ReviewsControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(int)), typeof(ValidateAntiForgeryTokenAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(int)), typeof(AjaxOnlyAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveHttpPostAttribute()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(int)), typeof(HttpDeleteAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [TestCase(0)]
        [TestCase(991)]
        [TestCase(-1)]
        public void Call_ReviewServiceGetByIdMethodOnceWithCorrectParams(int expectedId)
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(expectedId))
                         .Returns(new Mock<IBeerReview>().Object);
            // Act
            sut.Index(expectedId);

            // Assert
            reviewService.Verify(s => s.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenUserIsNotTheOwnerOfTheFoundReviewAndIsNotAdmin()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var beerReview = new Mock<IBeerReview>();

            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            // Act
            var result = sut.Index(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.StatusCode);
            StringAssert.Contains("edit other people", result.StatusDescription);
        }

        [TestCase(0)]
        [TestCase(991)]
        [TestCase(-1)]
        public void Call_ReviewServiceDeleteMethodOnceWithCorrectParams_WhenUserIsTheOwnerOfTheFoundReview(int expectedId)
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var beerReview = new Mock<IBeerReview>();
            beerReview.Setup(r => r.ApplicationUserId)
                      .Returns(this.expectedUserId);

            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);

            // Act
            sut.Index(expectedId);

            // Assert
            reviewService.Verify(s => s.DeleteReview(expectedId), Times.Once);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenReviewHasBeenDeleted()
        {
            // Arrange
            var sut = this.Kernel.Get<ReviewsController>(AjaxContextName);
            var beerReview = new Mock<IBeerReview>();
            beerReview.Setup(r => r.ApplicationUserId)
                      .Returns(this.expectedUserId);

            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);

            // Act
            var result = sut.Index(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            StringAssert.Contains("deleted successfully", result.StatusDescription);

        }

        public override void Init()
        {
            this.Kernel.Bind<HttpContextBase>()
                .ToMethod(ctx =>
                {
                    var request = new Mock<HttpRequestBase>();
                    request.SetupGet(x => x.Headers).Returns(
                                                             new WebHeaderCollection
                                                             {
                                                                           { "X-Requested-With", "XMLHttpRequest" }
                                                             });

                    var identity = new Mock<ClaimsIdentity>();
                    identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                            .Returns(new Claim("sa", this.expectedUserId));
                    var context = new Mock<HttpContextBase>();
                    context.SetupGet(x => x.Request).Returns(request.Object);
                    context.SetupGet(x => x.User.Identity).Returns(identity.Object);

                    return context.Object;
                })
                .InSingletonScope()
                .Named(AjaxContextName);

            base.Init();
        }
    }
}
