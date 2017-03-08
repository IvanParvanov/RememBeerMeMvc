using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models.Dtos;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Models.Dtos
{
    [TestFixture]
    public class BeerDtoTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetProperties()
        {
            var id = this.Fixture.Create<int>();
            var breweryId = this.Fixture.Create<int>();
            var name = this.Fixture.Create<string>();
            var breweryName = this.Fixture.Create<string>();

            var dto = new BeerDto();
            dto.Id = id;
            dto.BreweryId = breweryId;
            dto.Name = name;
            dto.BreweryName = breweryName;

            Assert.AreEqual(id, dto.Id);
            Assert.AreEqual(breweryId, dto.BreweryId);
            Assert.AreSame(name, dto.Name);
            Assert.AreSame(breweryName, dto.BreweryName);
        }
    }
}
