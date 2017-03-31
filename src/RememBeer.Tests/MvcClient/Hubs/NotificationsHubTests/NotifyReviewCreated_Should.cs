using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Hubs;
using RememBeer.MvcClient.Hubs.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Hubs.NotificationsHubTests.Base;

namespace RememBeer.Tests.MvcClient.Hubs.NotificationsHubTests
{
    [TestFixture]
    public class NotifyReviewCreated_Should : NotificationsHubNinjectTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();

        [Test]
        public async Task Call_ReviewService_GetLatestForUserIdMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var review = new Mock<IBeerReview>();
            review.SetupGet(r => r.User.UserName)
                  .Returns("");
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetLatestForUser(It.IsAny<string>()))
                         .Returns(review.Object);

            // Act
            await sut.NotifyReviewCreated();

            // Assert
            reviewService.Verify(s => s.GetLatestForUser(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_FollowerService_GetFollowersForUserIdAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var review = new Mock<IBeerReview>();
            review.SetupGet(r => r.User.UserName)
                  .Returns("");
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetLatestForUser(It.IsAny<string>()))
                         .Returns(review.Object);
            var followerService = this.MockingKernel.GetMock<IFollowerService>();

            // Act
            await sut.NotifyReviewCreated();

            // Assert
            followerService.Verify(f => f.GetFollowersForUserIdAsync(this.expectedUserId), Times.Once);
        }

        [Test]
        public async Task Call_Clients_UsersMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var clients = this.MockingKernel.GetMock<IHubCallerConnectionContext<INotificationsClient>>();

            var review = new Mock<IBeerReview>();
            review.SetupGet(r => r.User.UserName)
                  .Returns("");
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetLatestForUser(It.IsAny<string>()))
                         .Returns(review.Object);

            var foundUsers = this.GetMockedUsers(5);
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            followerService.Setup(f => f.GetFollowersForUserIdAsync(It.IsAny<string>()))
                           .Returns(Task.FromResult(foundUsers));
            var expectedIds = foundUsers.Select(u => u.Id).ToList();

            // Act
            await sut.NotifyReviewCreated();

            // Assert
            clients.Verify(c => c.Users(It.Is<IList<string>>(list => list.SequenceEqual(expectedIds))), Times.Once);
        }

        [Test]
        public async Task Call_Users_onFollowerReviewCreatedMethodOnceWithCorrectParams()
        {
            // Arrange
            const int expectedId = 991;
            const string expectedUsername = "peshooasjklasdjkasdas";

            var sut = this.MockingKernel.Get<NotificationsHub>();
            var notificationClients = this.MockingKernel.GetMock<INotificationsClient>();

            var review = new Mock<IBeerReview>();
            review.SetupGet(r => r.User.UserName)
                  .Returns(expectedUsername);
            review.SetupGet(r => r.Id)
                  .Returns(expectedId);
            var reviewService = this.MockingKernel.GetMock<IBeerReviewService>();
            reviewService.Setup(r => r.GetLatestForUser(It.IsAny<string>()))
                         .Returns(review.Object);

            // Act
            await sut.NotifyReviewCreated();

            // Assert
            notificationClients.Verify(m => m.OnFollowerReviewCreated(expectedId, expectedUsername), Times.Once);
        }

        public override void Init()
        {
            base.Init();

            this.MockingKernel.Bind<ClaimsIdentity>()
                .ToMethod(ctx =>
                          {
                              var identity = new Mock<ClaimsIdentity>();
                              identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                                      .Returns(new Claim("sa", this.expectedUserId));

                              return identity.Object;
                          });
        }
    }
}
