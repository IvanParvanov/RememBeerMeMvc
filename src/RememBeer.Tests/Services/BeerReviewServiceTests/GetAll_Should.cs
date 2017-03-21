using NUnit.Framework;

using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Services.BeerReviewServiceTests
{
    [TestFixture]
    public class GetAll_Should : TestClassBase
    {
        //[Test]
        //public void Call_RepositoryGetAllMethodWithCorrectParams_WhenNotPaginated()
        //{
        //    var expectedUserId = this.Fixture.Create<string>();
        //    var mockedRepository = new Mock<IEfRepository<BeerReview>>();
        //    var reviewService = new BeerReviewService(mockedRepository.Object);

        //    Expression<Func<BeerReview, bool>> b = ( x => x.IsDeleted == false && x.ApplicationUserId == expectedUserId );
        //    var expectedFilter = It.Is<Expression<Func<BeerReview, bool>>>(expr =>
        //                                                                       expr.ToString()
        //                                                                       .Contains("IsDeleted == False"));

        //    var expectedSort =
        //        It.Is<Expression<Func<BeerReview, DateTime>>>(expr => expr.ToString().Contains("CreatedAt"));

        //    reviewService.GetReviewsForUser(expectedUserId);

        //    mockedRepository.Verify(r => r.GetAll(
        //                                          It.IsAny<Expression<Func<BeerReview, bool>>>(),
        //                                          //expectedFilter,
        //                                          //It.IsAny<Expression<Func<BeerReview, DateTime>>>(),
        //                                          expectedSort,
        //                                          SortOrder.Descending
        //                                         ));
        //}
    }
}
