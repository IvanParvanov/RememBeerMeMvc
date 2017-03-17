using System;

using NUnit.Framework;

using RememBeer.MvcClient.Areas.Admin.Models;

namespace RememBeer.Tests.MvcClient.Models.Admin
{
    [TestFixture]
    public class CreateBeerBindingModelTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var sut = new CreateBeerBindingModel();
            var id = 902908234;
            var typeId = 23908321;
            var beerName = Guid.NewGuid().ToString();

            // Act
            sut.Id = id;
            sut.TypeId = typeId;
            sut.BeerName = beerName;

            // Assert
            Assert.AreSame(beerName, sut.BeerName);
            Assert.AreEqual(id, sut.Id);
            Assert.AreEqual(typeId, sut.TypeId);
        }
    }
}
