using System;
using System.Linq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Mvc.Models.Reviews
{
    [TestFixture]
    public class EditReviewBindingModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedId = this.Fixture.Create<int>();
            var expectedOverall = this.Fixture.Create<int>();
            var expectedSmell = this.Fixture.Create<int>();
            var expectedTaste = this.Fixture.Create<int>();
            var expectedLooks = this.Fixture.Create<int>();
            var expectedDescr = this.Fixture.Create<string>();
            var expectedPlace = this.Fixture.Create<string>();
            var sut = new EditReviewBindingModel();
            // Act
            sut.Id = expectedId;
            sut.Description = expectedDescr;
            sut.Overall = expectedOverall;
            sut.Look = expectedLooks;
            sut.Smell = expectedSmell;
            sut.Taste = expectedTaste;
            sut.Place = expectedPlace;
            // Assert
            Assert.AreEqual(expectedId, sut.Id);
            Assert.AreEqual(expectedOverall, sut.Overall);
            Assert.AreEqual(expectedLooks, sut.Look);
            Assert.AreEqual(expectedSmell, sut.Smell);
            Assert.AreEqual(expectedTaste, sut.Taste);
            Assert.AreSame(expectedPlace, sut.Place);
            Assert.AreSame(expectedDescr, sut.Description);
        }

        [Test]
        public void ScoreValues_ShouldBeOrderedFromOneToTen()
        {
            // Arrange & Act
            var values = EditReviewBindingModel.ScoreValues;
            var expected = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            // Assert
            Assert.That(values, Has.Count.EqualTo(10));
            Assert.That(values.Select(v => v.Value), Is.EqualTo(expected));
        }
    }
}
