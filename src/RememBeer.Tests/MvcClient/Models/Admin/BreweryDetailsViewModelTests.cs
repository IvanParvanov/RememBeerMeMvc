using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using RememBeer.Models;
using RememBeer.MvcClient.Areas.Admin.Models;

namespace RememBeer.Tests.MvcClient.Models.Admin
{
    [TestFixture]
    public class BreweryDetailsViewModelTests
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var sut = new BreweryDetailsViewModel();
            var createModel = new Mock<CreateBeerBindingModel>();
            var editModel = new Mock<EditBreweryBindingModel>();
            var beers = new List<Beer>();

            // Act
            sut.CreateModel = createModel.Object;
            sut.EditModel = editModel.Object;
            sut.Beers = beers;

            // Assert
            Assert.AreSame(beers, sut.Beers);
            Assert.AreSame(createModel.Object, sut.CreateModel);
            Assert.AreSame(editModel.Object, sut.EditModel);
        }
    }
}
