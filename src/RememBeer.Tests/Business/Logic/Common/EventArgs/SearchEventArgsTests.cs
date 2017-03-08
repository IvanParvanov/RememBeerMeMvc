using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Common.EventArgs;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Common.EventArgs
{
    [TestFixture]
    public class SearchEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetPropertiesCorrectly()
        {
            var expectedPattern = this.Fixture.Create<string>();

            var args = new SearchEventArgs(expectedPattern);

            Assert.AreSame(expectedPattern, args.Pattern);
        }
    }
}
