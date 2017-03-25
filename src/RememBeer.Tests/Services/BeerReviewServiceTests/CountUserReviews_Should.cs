using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Services;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BeerReviewServiceTests
{
    [TestFixture]
    public class CountUserReviews_Should : TestClassBase
    {
        [OneTimeSetUp]
        public void Init()
        {
            this.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => this.Fixture.Behaviors.Remove(b));
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(7)]
        public void ReturnTheCountOfNotDeletedReviewsByTheGivenUserId(int expectedCountForUser)
        {
            // Arrange
            var userId = this.Fixture.Create<string>();
            var allReviews = new List<BeerReview>();

            // Add random reviews which should not be counted
            for (var i = 0; i < expectedCountForUser * 2; i++ )
            {
                var review = this.Fixture.Create<BeerReview>();
                allReviews.Add(review);
            }

            // Add the reviews which should be counted
            for (var i = 0; i < expectedCountForUser; i++)
            {
                var userReview = this.Fixture.Create<BeerReview>();
                userReview.ApplicationUserId = userId;
                userReview.IsDeleted = false;
                allReviews.Add(userReview);
            }

            // Add deleted reviews which should not be counted
            for (var i = 0; i < expectedCountForUser / 2; i++)
            {
                var userReview = this.Fixture.Create<BeerReview>();
                userReview.ApplicationUserId = userId;
                userReview.IsDeleted = true;
                allReviews.Add(userReview);
            }

            var mockedRepository = new Mock<IEfRepository<BeerReview>>();
            mockedRepository.Setup(r => r.All)
                            .Returns(allReviews.AsQueryable);
            var beerReviewService = new BeerReviewService(mockedRepository.Object);

            // Act
            var actualCount = beerReviewService.CountUserReviews(userId);

            // Assert
            Assert.AreEqual(expectedCountForUser, actualCount);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(7)]
        public void ReturnTheCountOfNotDeletedReviewsByTheGivenUserIdAndSearch(int expectedCountForUser)
        {
            // Arrange
            var userId = this.Fixture.Create<string>();
            var allReviews = new List<BeerReview>();
            var expectedSearch = Guid.NewGuid().ToString();

            // Add random reviews which should not be counted
            for (var i = 0; i < expectedCountForUser * 2; i++)
            {
                var review = this.Fixture.Create<BeerReview>();
                allReviews.Add(review);
            }

            // Add the reviews which should be counted
            for (var i = 0; i < expectedCountForUser; i++)
            {
                var userReview = this.Fixture.Create<BeerReview>();
                userReview.ApplicationUserId = userId;
                userReview.IsDeleted = false;
                if (i % 3 == 0)
                {
                    // When name contains search
                    userReview.Beer.Name = "dsadsa" + expectedSearch + "54***dasj";
                }
                else if (i % 3 == 1)
                {
                    // When brewery name contains search
                    userReview.Beer.Brewery.Name = "dsadsa" + expectedSearch + "54***dasj";
                }
                else
                {
                    // When place contains search
                    userReview.Place = "dsadsa" + expectedSearch + "54***dasj";
                }

                allReviews.Add(userReview);
            }

            // Add deleted reviews which should not be counted
            for (var i = 0; i < expectedCountForUser / 2; i++)
            {
                var userReview = this.Fixture.Create<BeerReview>();
                userReview.ApplicationUserId = userId;
                userReview.IsDeleted = true;
                allReviews.Add(userReview);
            }

            var mockedRepository = new Mock<IEfRepository<BeerReview>>();
            mockedRepository.Setup(r => r.All)
                            .Returns(allReviews.AsQueryable);
            var beerReviewService = new BeerReviewService(mockedRepository.Object);

            // Act
            var actualCount = beerReviewService.CountUserReviews(userId, expectedSearch);

            // Assert
            Assert.AreEqual(expectedCountForUser, actualCount);
        }
    }
}
