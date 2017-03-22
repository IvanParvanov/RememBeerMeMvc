using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public class GetFollowersForUserIdAsync_Should : FollowerServiceNinjectBase
    {
        [TestCase(null)]
        [TestCase("sadasd89797896786A*SD^78^SD&*(^as89d87s")]
        public async Task ReturnNull_WhenUserIsNotFound(string userId)
        {
            // Arrange
            var users = new List<ApplicationUser>
                        {
                            new Mock<ApplicationUser>().Object,
                            new Mock<ApplicationUser>().Object,
                            new Mock<ApplicationUser>().Object
                        }.AsQueryable();

            var dbSet = this.GetMockAsyncDbSet(users);

            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);

            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = await sut.GetFollowersForUserIdAsync(userId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ReturnCoorrectListOfApplciationUsers_WhenUserIsFound()
        {
            // Arrange
            var expectedFollowers = new List<ApplicationUser>();
            var foundUser = new Mock<ApplicationUser>();
            foundUser.Setup(u => u.Followers)
                     .Returns(expectedFollowers);
            foundUser.Setup(u => u.Id)
                     .Returns("sdaasdasdasdasds");

            var users = new List<ApplicationUser>
                        {
                            new Mock<ApplicationUser>().Object,
                            new Mock<ApplicationUser>().Object,
                            foundUser.Object,
                            new Mock<ApplicationUser>().Object
                        }.AsQueryable();

            var dbSet = this.GetMockAsyncDbSet(users);
            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);

            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = await sut.GetFollowersForUserIdAsync(foundUser.Object.Id);

            // Assert
            Assert.AreSame(expectedFollowers, result);
        }
    }
}
