using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Common.EventArgs;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Common.EventArgs
{
    public class PaginationEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetPropertiesCorrectly()
        {
            var start = this.Fixture.Create<int>();
            var pageSize = this.Fixture.Create<int>();

            var sut = new PaginationEventArgs(start, pageSize);

            Assert.AreEqual(start, sut.StartRowIndex);
            Assert.AreEqual(pageSize, sut.PageSize);
        }
    }
}
