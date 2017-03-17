using System;

using NUnit.Framework;

using RememBeer.MvcClient.Areas.Admin.Models;

namespace RememBeer.Tests.MvcClient.Models.Admin
{
    [TestFixture]
    public class EditBreweryBindingModelTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var sut = new EditBreweryBindingModel();
            var id = 902908234;
            var typeId = 23908321;
            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var country = Guid.NewGuid().ToString();

            // Act
            sut.Id = id;
            sut.Description = description;
            sut.Name = name;
            sut.Country = country;

            // Assert
            Assert.AreSame(name, sut.Name);
            Assert.AreSame(description, sut.Description);
            Assert.AreEqual(country, sut.Country);
            Assert.AreEqual(id, sut.Id);
        }
    }
}
