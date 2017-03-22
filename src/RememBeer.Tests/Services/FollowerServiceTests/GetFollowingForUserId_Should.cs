using System.Collections.Generic;
using System.Linq;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Services.FollowerServiceTests.Base;

namespace RememBeer.Tests.Services.FollowerServiceTests
{
    [TestFixture]
    public class GetFollowingForUserIdAsync_Should : FollowerServiceNinjectBase
    {
        [TestCase(null)]
        [TestCase("sadasd89797896786A*SD^78^SD&*(^as89d87s")]
        public void Return_EmptyCollection_WhenUserIsNotFound(string userId)
        {
            // Arrange
            var followers = new List<ApplicationUser>();
            var mockedUsers = new Mock<ApplicationUser>();
            mockedUsers.Setup(u => u.Followers)
                       .Returns(followers);
            var users = new List<ApplicationUser>
                        {
                            mockedUsers.Object,
                            mockedUsers.Object
                        }.AsQueryable();

            var dbSet = this.GetMockAsyncDbSet(users);

            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);

            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = sut.GetFollowingForUserId(userId);

            // Assert
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void ReturnCorrectListOfApplciationUsers_WhenUserIsFound()
        {
            // Arrange
            var expectedId = "dsajihsadjkhjkasdasdds";
            var follower = new Mock<ApplicationUser>();
            follower.Setup(u => u.Id)
                .Returns(expectedId);
            var followers = new List<ApplicationUser>()
                                    {
                                        follower.Object
                                    };
            var foundUser = new Mock<ApplicationUser>();
            foundUser.Setup(u => u.Followers)
                     .Returns(followers);
            foundUser.Setup(u => u.Id)
                     .Returns("sdaasdasdasdasds");
            var fillerUsers = new Mock<ApplicationUser>();
            fillerUsers.Setup(x => x.Followers)
                       .Returns(new List<ApplicationUser>());
            var users = new List<ApplicationUser>
                        {
                            fillerUsers.Object,
                            fillerUsers.Object,
                            foundUser.Object,
                            fillerUsers.Object
                        }.AsQueryable();

            var dbSet = this.GetMockDbSet(users);
            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);

            var expectedResult = new List<ApplicationUser>() { foundUser.Object };
            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = sut.GetFollowingForUserId(expectedId);

            // Assert
            CollectionAssert.AreEquivalent(expectedResult, result);
        }
    }
}
