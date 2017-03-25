using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
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
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class ChangeImage_Should : ReviewsControllerTestBase
    {
        private const string ForCurrentUser = "ForCurrentUser";

        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [TestCase(typeof(ValidateAntiForgeryTokenAttribute))]
        [TestCase(typeof(AjaxOnlyAttribute))]
        [TestCase(typeof(HttpPostAttribute))]
        public void Have_RequiredAttributes(Type attrType)
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.ChangeImage(default(ChangeImageBindingModel)), attrType);

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public async Task Return_CorrectHttpStatusCodeResult_WhenUserIsNotTheOwnerOfTheFoundReviewAndIsNotAdmin()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = new Mock<IBeerReview>();

            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            // Act
            var result = await sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.StatusCode);
            StringAssert.Contains("edit other people", result.StatusDescription);
        }

        [Test]
        public async Task Call_ReviewServiceGetByIdMethodOnceWithCorrectparams_WhenUserIsTheOwnerOfTheFoundReview()
        {
            // Arrange
            var expectedId = 20;
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Id = expectedId,
                                   Image = fileMock.Object
                               };
            var beerReview = new Mock<IBeerReview>();

            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(expectedId))
                         .Returns(beerReview.Object);
            // Act
            await sut.ChangeImage(bindingModel);

            // Assert
            reviewService.Verify(r => r.GetById(expectedId), Times.Once);
        }

        [Test]
        public async Task Call_ImageUploadUploadImageMethodOnceWithCorrectParams_WhenUserIsTheOwnerOfTheFoundReview()
        {
            // Arrange
            var expectedStream = new MemoryStream();
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(expectedStream);
            var sut = this.MockingKernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.MockingKernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            var imgUploadServie = this.MockingKernel.GetMock<IImageUploadService>();

            // Act
            await sut.ChangeImage(bindingModel);

            // Assert
            imgUploadServie.Verify(s => s.UploadImageAsync(expectedStream, Constants.DefaultImageSizePx, Constants.DefaultImageSizePx), Times.Once);
        }

        [Test]
        public async Task Return_CorrectHttpStatusCodeResult_WhenImageUploadFails()
        {
            // Arrange
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.MockingKernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);

            // Act
            var result = await sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            StringAssert.Contains("could not be uploaded", result.StatusDescription);
        }

        [Test]
        public async Task SetReviewImgUrlPropertyToUploadImageReturnValueAndCallUpdateReview_WhenUploadIsSuccessful()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.MockingKernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var mockedReview = this.MockingKernel.GetMock<IBeerReview>();
            var imgUpload = this.MockingKernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(Task.FromResult(expectedUrl));
            // Act
            await sut.ChangeImage(bindingModel);

            // Assert
            mockedReview.VerifySet(r => r.ImgUrl = expectedUrl, Times.Once);
            reviewService.Verify(r => r.UpdateReview(beerReview), Times.Once);
        }

        [Test]
        public async Task ReturnCorrectResult_WhenUpdateFails()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.MockingKernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var imgUpload = this.MockingKernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(Task.FromResult(expectedUrl));
            // Act
            var result = await sut.ChangeImage(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            StringAssert.Contains("could not be uploaded", result.StatusDescription);
        }

        [Test]
        public async Task ReturnCorrectResult_WhenUpdateSucceeds()
        {
            // Arrange
            var expectedUrl = "I'm not an empty string";
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(new MemoryStream());
            var sut = this.MockingKernel.Get<ReviewsController>();
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful)
                        .Returns(true);
            var bindingModel = new ChangeImageBindingModel
                               {
                                   Image = fileMock.Object
                               };
            var beerReview = this.MockingKernel.Get<IBeerReview>(ForCurrentUser);
            var context = this.MockingKernel.Get<HttpContextBase>();
            sut.ControllerContext = new ControllerContext(context, new RouteData(), sut);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview);
            reviewService.Setup(r => r.UpdateReview(beerReview))
                         .Returns(updateResult.Object);
            var imgUpload = this.MockingKernel.GetMock<IImageUploadService>();
            imgUpload.Setup(i => i.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(Task.FromResult(expectedUrl));
            // Act
            var result = await sut.ChangeImage(bindingModel) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var viewModel = result.Data as UrlOnlyDto;
            Assert.IsNotNull(viewModel);
            Assert.AreSame(expectedUrl, viewModel.url);
        }

        public override void Init()
        {
            base.Init();

            this.MockingKernel.Bind<IBeerReview>().ToMethod(ctx =>
                                                            {
                                                                var review = new Mock<IBeerReview>();
                                                                review.Setup(r => r.ApplicationUserId)
                                                                      .Returns(this.expectedUserId);

                                                                return review.Object;
                                                            })
                .InSingletonScope()
                .Named(ForCurrentUser);

            this.MockingKernel.Rebind<HttpContextBase>()
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
    }
}
