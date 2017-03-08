using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Admin.ManageUsers;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Admin.ManageUsers.EventArgs
{
    [TestFixture]
    public class Ctor_Should : TestClassBase
    {
        [Test]
        public void SetUpPropertiesCorrectly()
        {
            var id = this.Fixture.Create<string>();
            var email = this.Fixture.Create<string>();
            var username = this.Fixture.Create<string>();
            var confirmed = this.Fixture.Create<bool>();

            var args = new UserUpdateEventArgs(id, email, username, confirmed);

            Assert.AreSame(id, args.Id);
            Assert.AreSame(email, args.Email);
            Assert.AreSame(username, args.UserName);
            Assert.AreEqual(confirmed, args.IsConfirmed);
        }
    }
}
