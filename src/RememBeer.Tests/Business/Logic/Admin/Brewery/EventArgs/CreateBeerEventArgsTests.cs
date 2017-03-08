using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.EventArgs
{
    [TestFixture]
    public class CreateBeerEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetPropertiesCorrectly()
        {
            var id = this.Fixture.Create<int>();
            var beerTypeId = this.Fixture.Create<int>();
            var beerName = this.Fixture.Create<string>();

            var args = new CreateBeerEventArgs(id, beerTypeId, beerName);

            Assert.AreEqual(id, args.Id);
            Assert.AreEqual(beerTypeId, args.BeerTypeId);
            Assert.AreSame(beerName, args.BeerName);
        }
    }
}
