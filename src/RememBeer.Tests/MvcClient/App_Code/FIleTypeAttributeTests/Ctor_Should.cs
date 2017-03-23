using System;

using NUnit.Framework;

using RememBeer.MvcClient.Attributes;

namespace RememBeer.Tests.MvcClient.App_Code.FIleTypeAttributeTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [TestCase("")]
        [TestCase("           ")]
        public void Throw_ArgumentException_WhenArgumentIsEmptyOrWhiteSpace(string invalid)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new FileTypeAttribute(invalid));
        }

        [Test]
        public void Throw_ArgumentNullException_WhenArgumentIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileTypeAttribute(null));
        }
    }
}

