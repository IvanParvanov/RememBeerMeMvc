using NUnit.Framework;

using Ploeh.AutoFixture;

using RememBeer.Business.Logic.Account.Register;
using RememBeer.Tests.Utils;

namespace RememBeer.Tests.Business.Logic.Account.Register.EventArgs
{
    [TestFixture]
    public class Ctor_Should : TestClassBase
    {
        [Test]
        public void SetPropertiesCorrectly()
        {
            var expectedName = this.Fixture.Create<string>();
            var expectedPassword = this.Fixture.Create<string>();
            var expectedEmail = this.Fixture.Create<string>();

            var args = new RegisterEventArgs(expectedName, expectedEmail, expectedPassword);

            Assert.AreSame(expectedName, args.UserName);
            Assert.AreSame(expectedEmail, args.Email);
            Assert.AreSame(expectedPassword, args.Password);
        }
    }
}
