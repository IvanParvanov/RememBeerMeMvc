using Moq;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Account.Confirm.Contracts;
using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Utils
{
    public static class MockedEventArgsGenerator
    {
        private static readonly Fixture Fixture = new Fixture();

        public static IIdentifiableEventArgs<T> GetIdentifiableEventArgs<T>()
        {
            var expectedId = Fixture.Create<T>();
            var args = new Mock<IIdentifiableEventArgs<T>>();
            args.Setup(a => a.Id)
                .Returns(expectedId);

            return args.Object;
        }

        public static IUserUpdateEventArgs GetMockedUserUpdateEventArgs()
        {
            var id = Fixture.Create<string>();
            var email = Fixture.Create<string>();
            var username = Fixture.Create<string>();
            var isConfirmed = Fixture.Create<bool>();
            var args = new Mock<IUserUpdateEventArgs>();
            args.Setup(a => a.Id)
                .Returns(id);
            args.Setup(a => a.Email)
                .Returns(email);
            args.Setup(a => a.UserName)
                .Returns(username);
            args.Setup(a => a.IsConfirmed)
                .Returns(isConfirmed);

            return args.Object;
        }

        public static ISearchEventArgs GetSearchEventArgs()
        {
            var expectedPattern = Fixture.Create<string>();
            var args = new Mock<ISearchEventArgs>();
            args.Setup(a => a.Pattern)
                .Returns(expectedPattern);

            return args.Object;
        }

        public static IBeerReviewInfoEventArgs GetBeerReviewInfoEventArgs()
        {
            var review = new Mock<IBeerReview>();
            var imageToUpload = new byte[50];
            var args = new Mock<IBeerReviewInfoEventArgs>();
            args.Setup(a => a.BeerReview)
                .Returns(review.Object);
            args.Setup(a => a.Image)
                .Returns(imageToUpload);

            return args.Object;
        }

        public static IUserReviewsEventArgs GetUserReviewsEventArgs()
        {
            var startRow = Fixture.Create<int>();
            var pageSize = Fixture.Create<int>();
            var userId = Fixture.Create<string>();

            var args = new Mock<IUserReviewsEventArgs>();
            args.Setup(a => a.PageSize)
                .Returns(pageSize);
            args.Setup(a => a.StartRowIndex)
                .Returns(startRow);
            args.Setup(a => a.UserId)
                .Returns(userId);

            return args.Object;
        }

        public static IConfirmEventArgs GetConfirmEventArgs()
        {
            var id = Fixture.Create<string>();
            var code = Fixture.Create<string>();
            var mockedArgs = new Mock<IConfirmEventArgs>();
            mockedArgs.Setup(a => a.UserId).Returns(id);
            mockedArgs.Setup(a => a.Code).Returns(code);

            return mockedArgs.Object;
        }
    }
}
