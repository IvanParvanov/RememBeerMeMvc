using System;
using System.Configuration;

using RememBeer.Common.Exceptions;

namespace RememBeer.Common.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public bool UserNamesAllowOnlyAlphanumeric => ParseBoolConfig(nameof(this.UserNamesAllowOnlyAlphanumeric));

        public bool RequireUniqueEmail => ParseBoolConfig(nameof(this.RequireUniqueEmail));

        public bool PasswordRequireNonLetterOrDigit => ParseBoolConfig(nameof(this.PasswordRequireNonLetterOrDigit));

        public bool PasswordRequireDigit => ParseBoolConfig(nameof(this.PasswordRequireDigit));

        public bool PasswordRequireLowercase => ParseBoolConfig(nameof(this.PasswordRequireLowercase));

        public bool PasswordRequireUppercase => ParseBoolConfig(nameof(this.PasswordRequireUppercase));

        public bool UserLockoutEnabledByDefault => ParseBoolConfig(nameof(this.UserLockoutEnabledByDefault));

        public int PasswordMinLength => ParseIntConfig(nameof(this.PasswordMinLength));

        public int DefaultAccountLockoutTimeSpan => ParseIntConfig(nameof(this.DefaultAccountLockoutTimeSpan));

        public int MaxFailedAccessAttemptsBeforeLockout => ParseIntConfig(nameof(this.MaxFailedAccessAttemptsBeforeLockout));

        public string ImageUploadName => GetConfigValue(nameof(this.ImageUploadName));

        public string ImageUploadApiKey => GetConfigValue(nameof(this.ImageUploadApiKey));

        public string ImageUploadApiSecret => GetConfigValue(nameof(this.ImageUploadApiSecret));

        public string MailSenderEmailAddress => GetConfigValue(nameof(this.MailSenderEmailAddress));

        public string MailUsername => GetConfigValue(nameof(this.MailUsername));

        public string MailPassword => GetConfigValue(nameof(this.MailPassword));

        private static bool ParseBoolConfig(string settingName)
        {
            try
            {
                var val = GetConfigValue(settingName);
                return bool.Parse(val);
            }
            catch (FormatException)
            {
                throw new InvalidConfigurationOptionException(settingName);
            }
        }

        private static int ParseIntConfig(string settingName)
        {
            try
            {
                var val = GetConfigValue(settingName);
                return int.Parse(val);
            }
            catch (FormatException)
            {
                throw new InvalidConfigurationOptionException(settingName);
            }
        }

        private static string GetConfigValue(string settingName)
        {
            var value = ConfigurationManager.AppSettings[settingName];
            if (value == null)
            {
                throw new MissingConfigurationOption(settingName);
            }

            return value;
        }
    }
}
