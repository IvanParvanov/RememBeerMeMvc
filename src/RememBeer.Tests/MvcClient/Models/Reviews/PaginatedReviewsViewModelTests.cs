using System.Collections.Generic;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.MvcClient.Models.Reviews
{
    [TestFixture]
    public class PaginatedReviewsViewModelTests : TestClassBase
    {
        [Test]
        public void Setters_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedCurrent = this.Fixture.Create<int>();
            var expectedPageSize = this.Fixture.Create<int>();
            var expectedTotal = this.Fixture.Create<int>();
            var expectedReviews = new List<SingleReviewViewModel>();
            var sut = new PaginatedReviewsViewModel();
            // Act
            sut.Items = expectedReviews;
            sut.CurrentPage = expectedCurrent;
            sut.PageSize = expectedPageSize;
            sut.TotalCount = expectedTotal;
            // Assert
            Assert.AreSame(expectedReviews, sut.Items);
            Assert.AreEqual(expectedCurrent, sut.CurrentPage);
            Assert.AreEqual(expectedPageSize, sut.PageSize);
            Assert.AreEqual(expectedTotal, sut.TotalCount);
        }
    }
}
