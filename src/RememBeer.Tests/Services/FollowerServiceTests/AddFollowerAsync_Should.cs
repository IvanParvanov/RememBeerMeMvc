﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Services.FollowerServiceTests.Base;

namespace RememBeer.Tests.Services.FollowerServiceTests
{
    [TestFixture]
    public class AddFollowerAsync_Should : FollowerServiceNinjectBase
    {
        [TestCase(null)]
        [TestCase("sadasd89797896786A*SD^78^SD&*(^as89d87s")]
        public async Task Return_FailedResult_WhenUserIsNotFound(string userId)
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
            var expectedResult = new Mock<IDataModifiedResult>();
            var factory = this.MockingKernel.GetMock<IDataModifiedResultFactory>();
            factory.Setup(f => f.CreateDatabaseUpdateResult(false, It.IsAny<IEnumerable<string>>()))
                   .Returns(expectedResult.Object);
            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = await sut.AddFollowerAsync(userId, "jihjkasd");

            // Assert
            Assert.AreSame(expectedResult.Object, result);
        }

        [TestCase("")]
        [TestCase("sadasd89797896786A*SD^78^SD&*(^as89d87s")]
        public async Task Return_FailedResult_WhenUserToFollowIsNotFound(string usernameToFollow)
        {
            // Arrange
            const string userId = "kdjsakljdaklsjkljasdasdd";
            var expectedFollowers = new List<ApplicationUser>();
            var foundUser = new Mock<ApplicationUser>();
            foundUser.Setup(u => u.Followers)
                     .Returns(expectedFollowers);
            foundUser.Setup(u => u.Id)
                     .Returns(userId);
            var expectedResult = new Mock<IDataModifiedResult>();
            var factory = this.MockingKernel.GetMock<IDataModifiedResultFactory>();
            factory.Setup(f => f.CreateDatabaseUpdateResult(false, It.IsAny<IEnumerable<string>>()))
                   .Returns(expectedResult.Object);

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
            var result = await sut.AddFollowerAsync(userId, usernameToFollow);

            // Assert
            Assert.AreSame(expectedResult.Object, result);
        }

        [Test]
        public async Task Add_FoundUserToFollowers()
        {
            // Arrange
            const string userId = "kdjsakljdaklsjkljasdasdd";
            const string userToFollowName = "peshoKASdjklasjdklj12323312123*()";

            var dataResult = new Mock<IDataModifiedResult>();
            var factory = this.MockingKernel.GetMock<IDataModifiedResultFactory>();
            factory.Setup(f => f.CreateDatabaseUpdateResult(false, It.IsAny<IEnumerable<string>>()))
                   .Returns(dataResult.Object);

            var foundUser = new Mock<ApplicationUser>();
            foundUser.Setup(u => u.Id)
                     .Returns(userId);
            var expectedFollowers = new List<ApplicationUser>();

            var userToFollow = new Mock<ApplicationUser>();
            userToFollow.Setup(u => u.Followers)
                        .Returns(expectedFollowers);
            userToFollow.Setup(u => u.UserName)
                        .Returns(userToFollowName);
            var users = new List<ApplicationUser>
                        {
                            new Mock<ApplicationUser>().Object,
                            new Mock<ApplicationUser>().Object,
                            foundUser.Object,
                            new Mock<ApplicationUser>().Object,
                            userToFollow.Object
                        }.AsQueryable();

            var dbSet = this.GetMockAsyncDbSet(users);
            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);

            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            await sut.AddFollowerAsync(userId, userToFollowName);

            // Assert
            CollectionAssert.Contains(userToFollow.Object.Followers, foundUser.Object);
        }

        [Test]
        public async Task Call_SaveChangesMethod_AndReturnCorrectResult()
        {
            // Arrange
            const string userId = "kdjsakljdaklsjkljasdasdd";
            const string userToFollowName = "peshoKASdjklasjdklj12323312123*()";

            var expectedResult = new Mock<IDataModifiedResult>();
            var factory = this.MockingKernel.GetMock<IDataModifiedResultFactory>();
            factory.Setup(f => f.CreateDatabaseUpdateResult(true, null))
                   .Returns(expectedResult.Object);

            var foundUser = new Mock<ApplicationUser>();
            foundUser.Setup(u => u.Id)
                     .Returns(userId);
            var expectedFollowers = new List<ApplicationUser>();

            var userToFollow = new Mock<ApplicationUser>();
            userToFollow.Setup(u => u.Followers)
                        .Returns(expectedFollowers);
            userToFollow.Setup(u => u.UserName)
                        .Returns(userToFollowName);
            var users = new List<ApplicationUser>
                        {
                            new Mock<ApplicationUser>().Object,
                            new Mock<ApplicationUser>().Object,
                            foundUser.Object,
                            new Mock<ApplicationUser>().Object,
                            userToFollow.Object
                        }.AsQueryable();

            var dbSet = this.GetMockAsyncDbSet(users);
            var db = this.MockingKernel.GetMock<IUsersDb>();
            db.Setup(d => d.Users)
              .Returns(dbSet.Object);
            var usersDb = this.MockingKernel.GetMock<IUsersDb>();

            var sut = this.MockingKernel.Get<FollowerService>();

            // Act
            var result = await sut.AddFollowerAsync(userId, userToFollowName);

            // Assert
            usersDb.Verify(d => d.SaveChangesAsync(), Times.Once);
            Assert.AreSame(expectedResult.Object, result);
        }
    }
}
