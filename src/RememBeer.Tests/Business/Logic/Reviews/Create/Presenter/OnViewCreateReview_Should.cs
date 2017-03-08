using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.Create;
using RememBeer.Business.Logic.Reviews.Create.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Common.Constants;
using RememBeer.Common.Services.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Business.Logic.Reviews.Create.Presenter
{
    [TestFixture]
    public class OnViewCreateReview_Should : TestClassBase
    {
        [Test]
        public void Call_ImgServiceUploadImageMethodOnceWithCorrectParams_WhenImageIsNotNull()
        {
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();
            var view = new Mock<ICreateReviewView>();
            var imgUpload = new Mock<IImageUploadService>();
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args);

            imgUpload.Verify(i => i.UploadImage(args.Image, Constants.DefaultThumbnailSizePx, Constants.DefaultThumbnailSizePx), Times.Once);
        }

        [Test]
        public void NotCall_ImgServiceUploadImageMethod_WhenImageIsNull()
        {
            var view = new Mock<ICreateReviewView>();
            var imgUpload = new Mock<IImageUploadService>();
            var review = new Mock<IBeerReview>();
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            createReviewResult.Setup(r => r.Errors)
                              .Returns(new string[0]);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);

            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);
            args.Setup(a => a.Image)
                .Returns((byte[])null);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args.Object);

            imgUpload.Verify(i => i.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Set_ReviewImgUrlToReturnValueFromUploadImage_WhenUrlIsNotNull()
        {
            var expectedUrl = this.Fixture.Create<string>();

            var view = new Mock<ICreateReviewView>();
            var review = new Mock<IBeerReview>();
            var imageToUpload = new byte[50];
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            createReviewResult.Setup(r => r.Errors)
                              .Returns(new string[0]);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);
            var imgUpload = new Mock<IImageUploadService>();
            imgUpload.Setup(img => img.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns(expectedUrl);
            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);
            args.Setup(a => a.Image)
                .Returns(imageToUpload);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args.Object);

            review.VerifySet(r => r.ImgUrl = expectedUrl, Times.Once);
        }

        [Test]
        public void Set_ReviewImgUrlToSelf_WhenUrlIsNull()
        {
            var view = new Mock<ICreateReviewView>();
            var review = new Mock<IBeerReview>();
            var imageToUpload = new byte[50];
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            createReviewResult.Setup(r => r.Errors)
                              .Returns(new string[0]);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);
            var imgUpload = new Mock<IImageUploadService>();
            imgUpload.Setup(img => img.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns((string)null);

            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);
            args.Setup(a => a.Image)
                .Returns(imageToUpload);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args.Object);

            review.VerifySet(r => r.ImgUrl = r.ImgUrl, Times.Once);
        }

        [Test]
        public void Call_ReviewServiceCreateReviewMethodOnceWithCorrectParams()
        {
            var view = new Mock<ICreateReviewView>();
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            createReviewResult.Setup(r => r.Errors)
                              .Returns(new string[0]);
            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);
            var imgUpload = new Mock<IImageUploadService>();
            imgUpload.Setup(img => img.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns((string)null);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args);

            reviewService.Verify(s => s.CreateReview(args.BeerReview), Times.Once);
        }

        [Test]
        public void Set_ViewErrorMessageText_WhenCreateReviewFails()
        {
            var expectedMessage = this.Fixture.Create<string>();
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();
            var view = new Mock<ICreateReviewView>();
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(false);
            createReviewResult.Setup(r => r.Errors)
                              .Returns(new[] { expectedMessage });

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);
            var imgUpload = new Mock<IImageUploadService>();
            imgUpload.Setup(img => img.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns((string)null);

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object);
            view.Raise(v => v.OnCreateReview += null, view.Object, args);

            view.VerifySet(v => v.ErrorMessageText = expectedMessage, Times.Once);
        }

        [Test]
        public void RedirectToCorrectUrl_WhenCreateReviewIsSuccessful()
        {
            var args = MockedEventArgsGenerator.GetBeerReviewInfoEventArgs();
            var view = new Mock<ICreateReviewView>();
            var createReviewResult = new Mock<IDataModifiedResult>();
            createReviewResult.Setup(r => r.Successful)
                              .Returns(true);

            var reviewService = new Mock<IBeerReviewService>();
            reviewService.Setup(r => r.CreateReview(It.IsAny<IBeerReview>()))
                         .Returns(createReviewResult.Object);
            var imgUpload = new Mock<IImageUploadService>();
            imgUpload.Setup(img => img.UploadImage(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                     .Returns((string)null);
            var mockedResponse = new MockedHttpResponse();

            var presenter = new CreateReviewPresenter(reviewService.Object, imgUpload.Object, view.Object)
                            {
                                HttpContext = new MockedHttpContextBase(mockedResponse)
                            };

            view.Raise(v => v.OnCreateReview += null, view.Object, args);

            Assert.AreEqual("/Reviews/My", mockedResponse.RedirectUrl);
        }
    }
}
