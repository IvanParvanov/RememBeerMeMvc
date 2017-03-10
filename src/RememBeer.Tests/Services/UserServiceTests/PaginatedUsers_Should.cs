using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Factories;
using RememBeer.Models.Identity.Contracts;
using RememBeer.Services;
using RememBeer.Tests.Utils;
using RememBeer.Tests.Utils.MockedClasses;

namespace RememBeer.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class PaginatedUsers_Should : TestClassBase
    {
        [TestCase(2, 4, 15)]
        [TestCase(0, 4, 15)]
        [TestCase(4, 4, 20)]
        [TestCase(0, 10, 15)]
        public void ReturnCorrectPartOfTheCollectionInOrder(int currentPage,
                                                            int expectedPageSize,
                                                            int expectedTotalCount)
        {
            // Arrange
            var usernameComparer = Comparer<ApplicationUser>.Create((a, b) =>
                                                                        string.Compare(a.UserName,
                                                                                       b.UserName,
                                                                                       StringComparison.Ordinal));

            var users = new List<MockedApplicationUser>();
            for (var i = 0; i < expectedTotalCount; i++)
            {
                users.Add(new MockedApplicationUser()
                          {
                              UserName = this.Fixture.Create<string>()
                          });
            }

            var queryableUsers = users.AsQueryable();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(r => r.Users)
                       .Returns(queryableUsers);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();
            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);
            int actualTotal;

            // Act
            var result = service.PaginatedUsers(currentPage, expectedPageSize, out actualTotal);

            var actualUsers = result as IApplicationUser[] ?? result.ToArray();
            var actualCount = actualUsers.Length;

            // Assert
            Assert.AreEqual(expectedTotalCount, actualTotal);
            Assert.AreEqual(expectedPageSize, actualCount);
            CollectionAssert.IsOrdered(actualUsers, usernameComparer);
        }

        [TestCase(2, 4, 15)]
        [TestCase(0, 4, 15)]
        [TestCase(4, 4, 20)]
        [TestCase(0, 10, 15)]
        public void ReturnCorrectPartOfTheCollectionInOrder_WhenSearching(int currentPage,
                                                                          int expectedPageSize,
                                                                          int totalCount)
        {
            // Arrange
            var usernameComparer = Comparer<ApplicationUser>.Create((a, b) =>
                                                                        string.Compare(a.UserName,
                                                                                       b.UserName,
                                                                                       StringComparison.Ordinal));
            var expectedFoundCount = totalCount / 2;

            var searchPattern = this.Fixture.Create<string>();

            var users = new List<MockedApplicationUser>();
            for (var i = 0; i < totalCount - expectedFoundCount; i++)
            {
                users.Add(new MockedApplicationUser()
                          {
                              UserName = this.Fixture.Create<string>()
                          });
            }

            for (var i = 0; i < expectedFoundCount; i++)
            {
                users.Add(new MockedApplicationUser()
                          {
                              UserName = this.Fixture.Create<string>() + searchPattern + this.Fixture.Create<string>()
                          });
            }

            var queryableUsers = users.AsQueryable();
            var userManager = new Mock<IApplicationUserManager>();
            userManager.Setup(r => r.Users)
                       .Returns(queryableUsers);

            var signInManager = new Mock<IApplicationSignInManager>();
            var modelFactory = new Mock<IModelFactory>();
            var service = new UserService(userManager.Object,
                                          signInManager.Object,
                                          modelFactory.Object);
            int actualTotal;

            // Act
            var result = service.PaginatedUsers(currentPage, expectedPageSize, out actualTotal, searchPattern);

            var actualUsers = result as IApplicationUser[] ?? result.ToArray();
            var actualCount = actualUsers.Length;

            // Assert
            Assert.AreEqual(expectedFoundCount, actualTotal);
            Assert.GreaterOrEqual(expectedPageSize, actualCount);
            CollectionAssert.IsOrdered(actualUsers, usernameComparer);
        }
    }
}
