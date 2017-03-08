using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using RememBeer.Business.Logic.Admin.UserDetails;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Business.Logic.Admin.UserDetails.ViewModel
{
    public class Setters_Should
    {
        [Test]
        public void SetPropertiesCorrectly()
        {
            var user = new Mock<IApplicationUser>().Object;
            var reviews = new List<IBeerReview>();

            var sut = new UserDetailsViewModel();
            sut.Reviews = reviews;
            sut.User = user;

            Assert.AreSame(user, sut.User);
            Assert.AreSame(reviews, sut.Reviews);
        }
    }
}
