using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.Models.Factories;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Models.Factories
{
    [TestFixture]
    public class ModelFactoryTests : TestClassBase
    {
        [Test]
        public void CreateApplicationUser_ShouldReturnApplicationUser()
        {
            var username = this.Fixture.Create<string>();
            var email = this.Fixture.Create<string>();

            var factory = new ModelFactory();
            var user = factory.CreateApplicationUser(username, email);

            Assert.IsNotNull(user);
            Assert.IsInstanceOf<ApplicationUser>(user);
            Assert.AreSame(username, user.UserName);
            Assert.AreSame(email, user.Email);
        }

        [Test]
        public void CreateBeerRank_ShouldReturnCorrectBeerRank()
        {
            var overallScore = this.Fixture.Create<decimal>();
            var tasteScore = this.Fixture.Create<decimal>();
            var looksScore = this.Fixture.Create<decimal>();
            var smellScore = this.Fixture.Create<decimal>();
            var totalReviews = this.Fixture.Create<int>();
            var compositeScore = this.Fixture.Create<decimal>();
            var beer = new Mock<IBeer>();

            var rank = new ModelFactory().CreateBeerRank(overallScore,
                                                         tasteScore,
                                                         looksScore,
                                                         smellScore,
                                                         beer.Object,
                                                         compositeScore,
                                                         totalReviews);

            Assert.IsNotNull(rank);
            Assert.IsInstanceOf<BeerRank>(rank);
            Assert.AreEqual(overallScore, rank.OverallScore);
            Assert.AreEqual(tasteScore, rank.TasteScore);
            Assert.AreEqual(looksScore, rank.LookScore);
            Assert.AreEqual(smellScore, rank.SmellScore);
            Assert.AreEqual(compositeScore, rank.CompositeScore);
            Assert.AreEqual(totalReviews, rank.TotalReviews);
            Assert.AreSame(beer.Object, rank.Beer);
        }
    }
}
