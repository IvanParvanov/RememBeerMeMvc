using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.Brewery.EventArgs
{
    [TestFixture]
    public class UpdateEventArgsTests : TestClassBase
    {
        [Test]
        public void Ctor_ShouldSetUpPropertiesCorrectly()
        {
            var id = this.Fixture.Create<int>();
            var descr = this.Fixture.Create<string>();
            var name = this.Fixture.Create<string>();
            var country = this.Fixture.Create<string>();

            var args = new BreweryUpdateEventArgs(id, descr, name, country);

            Assert.AreEqual(id, args.Id);
            Assert.AreSame(descr, args.Description);
            Assert.AreSame(name, args.Name);
            Assert.AreSame(country, args.Country);
        }
    }
}
