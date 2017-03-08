using System;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Models
{
    [TestFixture]
    public class BeerReviewTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetUpPropertiesCorrectly()
        {
            var expectedId = this.Fixture.Create<int>();
            var expectedText = this.Fixture.Create<string>();
            var isPublic = true;
            var expectedDate = DateTime.Now;
            var review = new BeerReview()
                         {
                             Id = expectedId,
                             BeerId = expectedId,
                             ApplicationUserId = expectedText,
                             IsDeleted = isPublic,
                             Overall = expectedId,
                             Look = expectedId,
                             Smell = expectedId,
                             Taste = expectedId,
                             Description = expectedText,
                             IsPublic = isPublic,
                             Place = expectedText,
                             Beer = null,
                             CreatedAt = expectedDate,
                             ModifiedAt = expectedDate,
                             User = null,
                             ImgUrl = expectedText
                         };

            Assert.AreEqual(expectedId, review.Id);
            Assert.AreEqual(expectedId, review.Overall);
            Assert.AreEqual(expectedId, review.Look);
            Assert.AreEqual(expectedId, review.BeerId);
            Assert.AreEqual(expectedId, review.Smell);
            Assert.AreEqual(expectedId, review.Taste);
            Assert.AreEqual(isPublic, review.IsPublic);
            Assert.AreEqual(isPublic, review.IsDeleted);

            Assert.AreEqual(expectedText, review.ApplicationUserId);
            Assert.AreSame(expectedText, review.Description);
            Assert.AreSame(expectedText, review.Place);
            Assert.AreSame(expectedText, review.ImgUrl);
            Assert.AreSame(null, review.Beer);
            Assert.AreSame(null, review.User);
            Assert.AreEqual(expectedDate, review.CreatedAt);
            Assert.AreEqual(expectedDate, review.ModifiedAt);
        }
    }
}
