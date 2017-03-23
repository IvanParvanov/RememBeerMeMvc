using System;
using System.Web;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Attributes;

namespace RememBeer.Tests.MvcClient.App_Code.FIleTypeAttributeTests
{
    [TestFixture]
    public class IsValid_Should
    {
        [TestCase(".jpg", ".jpg", true)]
        [TestCase(".png, .jpg, .gif", ".jpg", true)]
        [TestCase(".png, .jpg,.pesho", ".pesho", true)]
        [TestCase(".png,.jpg,.pesho", ".pesho", true)]
        [TestCase(".png,.jpg,.gif", ".pesho", false)]
        [TestCase(".png, .jpg, .gif", ".pesho", false)]
        public void ReturnExpectedResult_WhenValueIsHttpPostedFileBase(string allowedExtensions, string actualExtension, bool expected)
        {
            // Arrange
            var sut = new FileTypeAttribute(allowedExtensions);
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.FileName)
                .Returns(Guid.NewGuid() + actualExtension);

            // Act
            var result = sut.IsValid(file.Object);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(2)]
        [TestCase("sadasdasdsasdasdas")]
        [TestCase(3.323232)]
        public void ReturnTrue_WhenValueIsNotHttpPostedFileBase(object value)
        {
            // Arrange
            var sut = new FileTypeAttribute(".gif");

            // Act
            var result = sut.IsValid(value);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
