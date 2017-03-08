using NUnit.Framework;

using RememBeer.Business.Logic.Account.ManagePassword;

namespace RememBeer.Tests.Business.Logic.Account.ManagePassword.EventArgs
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void SetPropertiesCorrectly()
        {
            var current = "asd@abv.bg";
            var newPass = "password123";
            var userId = "897sad89&D*(&AS*(D7a(S*Dasdjkasdhasjkhasdk";

            var args = new ChangePasswordEventArgs(current, newPass, userId);

            Assert.AreSame(current, args.CurrentPassword);
            Assert.AreSame(newPass, args.NewPassword);
            Assert.AreEqual(userId, args.UserId);
        }
    }
}
