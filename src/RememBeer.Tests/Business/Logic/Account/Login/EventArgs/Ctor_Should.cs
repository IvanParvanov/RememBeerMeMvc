using NUnit.Framework;

using RememBeer.Business.Logic.Account.Login;

namespace RememBeer.Tests.Business.Logic.Account.Login.EventArgs
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void SetPropertiesCorrectly()
        {
            var email = "asd@abv.bg";
            var password = "password123";
            var rememberMe = false;

            var args = new LoginEventArgs(email, password, rememberMe);

            Assert.AreSame(email, args.Email);
            Assert.AreSame(password, args.Password);
            Assert.AreEqual(rememberMe, args.RememberMe);
        }
    }
}
