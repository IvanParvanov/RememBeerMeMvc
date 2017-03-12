using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Common.Services.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Mvc.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class ChangeImage_Should : ReviewsControllerTestBase
    {
        private const string ForCurrentUser = "ForCurrentUser";

        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            var sut = this.Kernel.Get<ReviewsController>();
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ChangeImage(default(ChangeImageBindingModel)), typeof(ValidateAntiForgeryTokenAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            var sut = this.Kernel.Get<ReviewsController>();
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ChangeImage(default(ChangeImageBindingModel)), typeof(AjaxOnlyAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveHttpPostAttribute()
        {
            var sut = this.Kernel.Get<ReviewsController>();
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ChangeImage(default(ChangeImageBindingModel)), typeof(HttpPostAttribute));
            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenUserIsNotTheOwnerOfTheFoundReviewAndIsNotAdmin()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = new Mock<IBeerReview>();

            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            // Act
            var result = sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.StatusCode);
            StringAssert.Contains("edit other people", result.StatusDescription);
        }

        [Test]
        public void Call_ReviewServiceGetByIdMethodOnceWithCorrectparams_WhenUserIsTheOwnerOfTheFoundReview()
        {
            // Arrange
            var expectedId = 20;
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Id = expectedId,
                                   Image = fileMock.Object
                               };
            var beerReview = new Mock<IBeerReview>();

            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(expectedId))
                         .Returns(beerReview.Object);
            // Act
            sut.ChangeImage(bindingModel);

            // Assert
            reviewService.Verify(r => r.GetById(expectedId), Times.Once);
        }

        [Test]
        public void Call_ImageUploadUploadImageMethodOnceWithCorrectParams_WhenUserIsTheOwnerOfTheFoundReview()
        {
            // Arrange
            var expectedByteArray = new byte[50];
            var expectedStream = new MemoryStream(expectedByteArray);
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(expectedStream);
            var sut = this.Kernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.Kernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            var imgUploadServie = this.Kernel.GetMock<IImageUploadService>();

            // Act
            sut.ChangeImage(bindingModel);

            // Assert
            imgUploadServie.Verify(s => s.UploadImage(It.Is<byte[]>(b => b.Length == 50), Constants.DefaultImageSizePx, Constants.DefaultImageSizePx), Times.Once);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenImageUploadFails()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
            {
                Image = fileMock.Object
            };
            var beerReview = this.Kernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);

            // Act
            var result = sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            StringAssert.Contains("could not be uploaded", result.StatusDescription);
        }

        [Test]
        public void SetReviewImgUrlPropertyToUploadImageReturnValueAndCallUpdateReview_WhenUploadIsSuccessful()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            var bindingModel = new ChangeImageBindingModel
            {
                Image = fileMock.Object
            };
            var beerReview = this.Kernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var mockedReview = this.Kernel.GetMock<IBeerReview>();
            var imgUpload = this.Kernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(expectedUrl);
            // Act
            sut.ChangeImage(bindingModel);

            // Assert
            mockedReview.VerifySet(r => r.ImgUrl = expectedUrl, Times.Once);
            reviewService.Verify(r => r.UpdateReview(beerReview), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenUpdateFails()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            var bindingModel = new ChangeImageBindingModel
            {
                Image = fileMock.Object
            };
            var beerReview = this.Kernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var imgUpload = this.Kernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(expectedUrl);
            // Act
            var result = sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            StringAssert.Contains("could not be uploaded", result.StatusDescription);
        }

        [Test]
        public void ReturnCorrectResult_WhenUpdateSucceeds()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.Kernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful)
                        .Returns(true);
            var bindingModel = new ChangeImageBindingModel
            {
                Image = fileMock.Object
            };
            var beerReview = this.Kernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.Kernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.Kernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var imgUpload = this.Kernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(expectedUrl);
            // Act
            var result = sut.ChangeImage(bindingModel) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var viewModel = result.Data as UrlOnlyDto;
            Assert.IsNotNull(viewModel);
            Assert.AreSame(expectedUrl, viewModel.url);
        }

        public override void Init()
        {
            base.Init();

            this.Kernel.Bind<IBeerReview>().ToMethod(ctx =>
                                                     {
                                                         var review = new Mock<IBeerReview>();
                                                         review.Setup(r => r.ApplicationUserId)
                                                               .Returns(this.expectedUserId);

                                                         return review.Object;
                                                     })
                .InSingletonScope()
                .Named(ForCurrentUser);

            this.Kernel.Rebind<HttpContextBase>()
                .ToMethod(ctx =>
                          {
                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity).Returns(identity.Object);
                              mockedUser.Setup(u => u.IsInRole(Constants.AdminRole))
                                        .Returns(false);
                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.User).Returns(mockedUser.Object);

                              return context.Object;
                          })
                .InSingletonScope();
        }

        private byte[] StreamToArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
