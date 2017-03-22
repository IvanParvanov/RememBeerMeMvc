using System.Security.Claims;

using Microsoft.AspNet.SignalR;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Hubs.UserIdProvider;

namespace RememBeer.Tests.MvcClient.Hubs.UserIdProviderTests
{
    [TestFixture]
    public class GetUserId_Should
    {
        [TestCase("")]
        [TestCase("jkasjkdjdas=321-99089jjhasuipesho1233890789sdjh")]
        public void Return_CorrectUserId(string expected)
        {
            // Arrange
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>()))
                    .Returns(new Claim("sa", expected));
            var request = new Mock<IRequest>();
            request.SetupGet(r => r.User.Identity)
                   .Returns(identity.Object);
            var sut = new UserIdProvider();

            // Act
            var actual = sut.GetUserId(request.Object);

            // Assert
            Assert.AreSame(expected, actual);
        }
    }
}
