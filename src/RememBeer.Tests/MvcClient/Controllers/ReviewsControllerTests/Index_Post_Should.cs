using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Common.Constants;
using RememBeer.Common.Services.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Controllers.Ninject;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.TestExtensions;

namespace RememBeer.Tests.MvcClient.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Index_Post_Should : ReviewsControllerTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [Test]
        public void HaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(CreateReviewBindingModel)), typeof(ValidateAntiForgeryTokenAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveAjaxOnlyAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(CreateReviewBindingModel)), typeof(AjaxOnlyAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveHttpPostAttribute()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();

            // Act
            var hasAttribute = AttributeTester.MethodHasAttribute(() => sut.Index(default(CreateReviewBindingModel)), typeof(HttpPostAttribute));

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenModelValidationFails()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>();
            var invalidModel = new CreateReviewBindingModel();

            // Act
            sut.ValidateViewModel(invalidModel);
            var result = sut.Index(invalidModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            StringAssert.Contains("validation failed", result.StatusDescription);
        }

        [Test]
        public void Call_ReviewServiceCreateReviewMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var bindingModel = new CreateReviewBindingModel();
            var expectedReview = new BeerReview();

            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(expectedReview))
                         .Returns(new Mock<IDataModifiedResult>().Object);
            // Act
            sut.Index(bindingModel);

            // Assert
            reviewService.Verify(s => s.CreateReview(expectedReview), Times.Once);
        }

        [Test]
        public void Call_IMapperMapMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var bindingModel = new CreateReviewBindingModel();

            var expectedReview = new BeerReview();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(new Mock<IDataModifiedResult>().Object);
            // Act
            sut.Index(bindingModel);

            // Assert
            mapper.Verify(m => m.Map(bindingModel, It.IsAny<BeerReview>()), Times.Once());
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenReviewCreationFails()
        {
            // Arrange
            var expectedErrors = new[] { "error1", "error2" };
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Errors).Returns(expectedErrors);

            // Arrange
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var bindingModel = new CreateReviewBindingModel();
            var beerReview = new Mock<IBeerReview>();

            var expectedReview = new BeerReview();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetById(It.IsAny<int>()))
                         .Returns(beerReview.Object);
            reviewService.Setup(r => r.CreateReview(expectedReview))
                         .Returns(updateResult.Object);
            // Act
            var result = sut.Index(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            StringAssert.Contains("validation failed", result.StatusDescription);
        }

        [Test]
        public void Return_CorrectResult_WhenReviewCreationSucceeds()
        {
            // Arrange
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful).Returns(true);

            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var bindingModel = new CreateReviewBindingModel();
            var expectedReview = new BeerReview();
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(updateResult.Object);

            // Act
            var result = sut.Index(bindingModel) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("My", result.RouteValues["action"]?.ToString());
            Assert.AreEqual(null, result.RouteValues["controller"]?.ToString());
        }

        [Test]
        public void Call_ImageServiceUploadImageMethodOnceWithCorrectParams_WhenImageIsNotNull()
        {
            // Arrange
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful).Returns(true);

            var expectedByteArray = new byte[50];
            var expectedStream = new MemoryStream(expectedByteArray);
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(expectedStream);
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var bindingModel = new CreateReviewBindingModel()
                               {
                                   Image = fileMock.Object
                               };

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(updateResult.Object);
            var imgUploadService = this.MockingKernel.GetMock<IImageUploadService>();

            // Act
            sut.Index(bindingModel);

            // Assert
            imgUploadService.Verify(s => s.UploadImage(It.Is<byte[]>(b => b.Length == 50), Constants.DefaultImageSizePx, Constants.DefaultImageSizePx), Times.Once);
        }

        [Test]
        public void NotCall_ImageServiceUploadImageMethodOnceWithCorrectParams_WhenImageIsNull()
        {
            // Arrange
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful).Returns(true);
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var expectedReview = new BeerReview();
            var bindingModel = new CreateReviewBindingModel()
                               {
                                   Image = null
                               };

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(updateResult.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);
            var imgUploadService = this.MockingKernel.GetMock<IImageUploadService>();

            // Act
            sut.Index(bindingModel);

            // Assert
            imgUploadService.Verify(s => s.UploadImage(It.IsAny<byte[]>(), Constants.DefaultImageSizePx, Constants.DefaultImageSizePx), Times.Never);
        }

        [Test]
        public void Return_CorrectHttpStatusCodeResult_WhenImageUploadFails()
        {
            // Arrange
            var updateResult = new Mock<IDataModifiedResult>();
            updateResult.Setup(r => r.Successful).Returns(true);
            var sut = this.MockingKernel.Get<ReviewsController>(AjaxContextName);
            var expectedReview = new BeerReview();

            var expectedByteArray = new byte[50];
            var expectedStream = new MemoryStream(expectedByteArray);
            var fileMock = new Mock<HttpPostedFileBase>();
            fileMock.Setup(m => m.InputStream)
                    .Returns(expectedStream);
            var bindingModel = new CreateReviewBindingModel()
                               {
                                   Image = fileMock.Object
            };

            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(s => s.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(updateResult.Object);
            var mapper = this.MockingKernel.GetMock<IMapper>();
            mapper.Setup(m => m.Map(bindingModel, It.IsAny<BeerReview>()))
                  .Returns(expectedReview);

            // Act
            var result = sut.Index(bindingModel) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
            StringAssert.Contains("could not be uploaded", result.StatusDescription);
        }

        public override void Init()
        {
            this.MockingKernel.Bind<HttpContextBase>()
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

                              var mockedUser = new Mock<IPrincipal>();
                              mockedUser.Setup(u => u.Identity).Returns(identity.Object);

                              var context = new Mock<HttpContextBase>();
                              context.SetupGet(x => x.Request).Returns(request.Object);
                              context.SetupGet(x => x.User).Returns(mockedUser.Object);

                              return context.Object;
                          })
                .InSingletonScope()
                .Named(AjaxContextName);

            base.Init();
        }
    }
}
