using NUnit.Framework;

using RememBeer.Business.Logic.Common.EventArgs;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Common.EventArgs
{
    [TestFixture]
    public class IdentifiableEventArgsTests : TestClassBase
    {
        [TestCase(1231654578)]
        [TestCase("klasdjasdljlasklas*(7789799sda8")]
        public void Ctor_ShouldSetPropertiesCorrectly<T>(T id)
        {
            var args = new IdentifiableEventArgs<T>(id);

            Assert.AreEqual(id, args.Id);
        }
    }
}
