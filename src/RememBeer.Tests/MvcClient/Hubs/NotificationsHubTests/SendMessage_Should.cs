using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.MvcClient.Hubs;
using RememBeer.MvcClient.Hubs.Contracts;
using RememBeer.Services.Contracts;
using RememBeer.Tests.MvcClient.Hubs.NotificationsHubTests.Base;

namespace RememBeer.Tests.MvcClient.Hubs.NotificationsHubTests
{
    [TestFixture]
    public class SendMessage_Should : NotificationsHubNinjectTestBase
    {
        private readonly string expectedUserId = Guid.NewGuid().ToString();
        private readonly string expectedUserName = Guid.NewGuid().ToString();

        [Test]
        public async Task Call_FollowerService_GetFollowersForUserIdAsyncMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var followerService = this.MockingKernel.GetMock<IFollowerService>();

            // Act
            await sut.SendMessage("notempty", null, null);

            // Assert
            followerService.Verify(f => f.GetFollowersForUserIdAsync(this.expectedUserId), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("                         ")]
        public async Task NotCall_AnyForeignMethods_WhenMessageIsNullOrWhitespace(string emptyMessage)
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            var clients = this.MockingKernel.GetMock<IHubCallerConnectionContext<INotificationsClient>>();
            var mockDynamic = this.MockingKernel.GetMock<INotificationsClient>();

            // Act
            await sut.SendMessage(emptyMessage, null, null);

            // Assert
            followerService.Verify(f => f.GetFollowersForUserIdAsync(It.IsAny<string>()), Times.Never);
            clients.Verify(c => c.Users(It.IsAny<IList<string>>()), Times.Never);
            mockDynamic.Verify(m => m.ShowNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Call_Clients_UsersMethodOnceWithCorrectParams()
        {
            // Arrange
            var sut = this.MockingKernel.Get<NotificationsHub>();
            var clients = this.MockingKernel.GetMock<IHubCallerConnectionContext<INotificationsClient>>();

            var foundUsers = this.GetMockedUsers(5);
            var followerService = this.MockingKernel.GetMock<IFollowerService>();
            followerService.Setup(f => f.GetFollowersForUserIdAsync(It.IsAny<string>()))
                           .Returns(Task.FromResult(foundUsers));
            var expectedIds = foundUsers.Select(u => u.Id).ToList();

            // Act
            await sut.SendMessage("notempty", null, null);

            // Assert
            clients.Verify(c => c.Users(It.Is<IList<string>>(list => list.SequenceEqual(expectedIds))), Times.Once);
        }

        [Test]
        public async Task Call_Users_onFollowerReviewCreatedMethodOnceWithCorrectParams()
        {
            // Arrange
            const string expectedMessage = "peshooasjklasdaasdjkasdas";
            const string expectedLon = "pesh123ooasjklasdjkasdas";
            const string expectedLat = "peshooasjklasdjk23125asdas";

            var sut = this.MockingKernel.Get<NotificationsHub>();
            var mockDynamic = this.MockingKernel.GetMock<INotificationsClient>();

            // Act
            await sut.SendMessage(expectedMessage, expectedLat, expectedLon);

            // Assert
            mockDynamic.Verify(m => m.ShowNotification(expectedMessage, this.expectedUserName, expectedLat, expectedLon), Times.Once);
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
                              identity.SetupGet(i => i.Name)
                                      .Returns(this.expectedUserName);

                              return identity.Object;
                          });
        }
    }
}
