using System.Configuration;

using NUnit.Framework;

using RememBeer.Common.Configuration;

namespace RememBeer.IntegrationTests.Common
{
    [TestFixture]
    public class ConfigurationProviderTests
    {
        [TestCase("ImageUploadName")]
        [TestCase("ImageUploadApiKey")]
        [TestCase("ImageUploadApiSecret")]
        [TestCase("MailSenderEmailAddress")]
        [TestCase("MailUsername")]
        [TestCase("MailPassword")]
        public void Properties_ShouldReturnValueFromApplicationConfiguration_WhenValueIsString(string settingName)
        {
            // Arrange
            var sut = new ConfigurationProvider();
            var expected = ConfigurationManager.AppSettings[settingName];

            // Act
            var actual = this.GetPropValue<string>(sut, settingName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("UserNamesAllowOnlyAlphanumeric")]
        [TestCase("RequireUniqueEmail")]
        [TestCase("PasswordRequireNonLetterOrDigit")]
        [TestCase("PasswordRequireDigit")]
        [TestCase("PasswordRequireLowercase")]
        [TestCase("PasswordRequireUppercase")]
        [TestCase("UserLockoutEnabledByDefault")]
        public void Properties_ShouldReturnValueFromApplicationConfiguration_WhenValueIsBool(string settingName)
        {
            // Arrange
            var sut = new ConfigurationProvider();
            var expected = bool.Parse(ConfigurationManager.AppSettings[settingName]);

            // Act
            var actual = this.GetPropValue<bool>(sut, settingName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("DefaultAccountLockoutTimeSpan")]
        [TestCase("MaxFailedAccessAttemptsBeforeLockout")]
        [TestCase("PasswordMinLength")]
        public void Properties_ShouldReturnValueFromApplicationConfiguration_WhenValueIsInt(string settingName)
        {
            // Arrange
            var sut = new ConfigurationProvider();
            var expected = int.Parse(ConfigurationManager.AppSettings[settingName]);

            // Act
            var actual = this.GetPropValue<int>(sut, settingName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private T GetPropValue<T>(object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
