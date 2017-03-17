using System.Collections.Generic;
using System.Web.Mvc;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Manage;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Manage
{
    [TestFixture]
    public class ConfigureTwoFactorViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedProvider = this.Fixture.Create<string>();
            var expectedProviders = new List<SelectListItem>();
            var sut = new ConfigureTwoFactorViewModel();
            // Act
            sut.SelectedProvider = expectedProvider;
            sut.Providers = expectedProviders;
            // Assert
            Assert.AreSame(expectedProvider, sut.SelectedProvider);
            Assert.AreSame(expectedProviders, sut.Providers);
        }
    }
}
