using System;

using Moq;

using NUnit.Framework;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services;

namespace RememBeer.Tests.Services.FollowerServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIUsersDbIsNull()
        {
            // Arrange
            var factory = new Mock<IDataModifiedResultFactory>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FollowerService(null, factory.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIDataModifiedResultFactoryIsNull()
        {
            // Arrange
            var usersDb = new Mock<IUsersDb>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FollowerService(usersDb.Object, null));
        }
    }
}
