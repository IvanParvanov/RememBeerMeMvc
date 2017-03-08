using System.Collections.Generic;

using NUnit.Framework;

using RememBeer.Business.Logic.Admin.ManageUsers;
using RememBeer.Models.Contracts;

namespace RememBeer.Tests.Business.Logic.Admin.ManageUsers.ViewModel
{
    [TestFixture]
    public class Setters_Should
    {
        [Test]
        public void SetPropertiesCorrectly()
        {
            var users = new List<IApplicationUser>();
            var viewModel = new ManageUsersViewModel();

            viewModel.Users = users;

            Assert.AreSame(users, viewModel.Users);
        }
    }
}
