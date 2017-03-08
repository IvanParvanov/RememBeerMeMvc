using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Reviews.My;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Reviews.My.EventArgs
{
    [TestFixture]
    public class UserReviewsEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetPropertiesCorrectly()
        {
            var startRow = this.Fixture.Create<int>();
            var pageSize = this.Fixture.Create<int>();
            var userId = this.Fixture.Create<string>();

            var sut = new UserReviewsEventArgs(startRow, pageSize, userId);

            Assert.AreSame(userId, sut.UserId);
            Assert.AreEqual(sut.StartRowIndex, startRow);
            Assert.AreEqual(sut.PageSize, pageSize);
        }
    }
}
