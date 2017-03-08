using NUnit.Framework;

using RememBeer.Business.Logic.Account.ForgotPassword;

namespace RememBeer.Tests.Business.Logic.Account.ForgotPassword.EventArgs
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void SetUpPropertiesCorrectly()
        {
            var email = "asd@abv.bg";

            var args = new ForgotPasswordEventArgs(email);

            Assert.AreSame(email, args.Email);
        }
    }
}
