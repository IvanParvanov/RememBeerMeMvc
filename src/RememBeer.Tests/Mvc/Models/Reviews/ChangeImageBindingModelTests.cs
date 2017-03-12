using System.Web;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Models.Reviews
{
    [TestFixture]
    public class ChangeImageBindingModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = this.Fixture.Create<int>();
            var expectedImage = new Mock<HttpPostedFileBase>();
            var sut = new ChangeImageBindingModel();
            // Act
            sut.Id = expectedId;
            sut.Image = expectedImage.Object;
            // Assert
            Assert.AreEqual(expectedId, sut.Id);
            Assert.AreSame(expectedImage.Object, sut.Image);
        }
    }
}
