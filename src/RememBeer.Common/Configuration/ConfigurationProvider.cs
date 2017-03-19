using System;
using System.Configuration;

using RememBeer.Common.Exceptions;

namespace RememBeer.Common.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public bool UserNamesAllowOnlyAlphanumeric
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.UserNamesAllowOnlyAlphanumeric));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.UserNamesAllowOnlyAlphanumeric));
                }
            }
        }

        public bool RequireUniqueEmail
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.RequireUniqueEmail));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.RequireUniqueEmail));
                }
            }
        }

        public int PasswordMinLength
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.PasswordMinLength));
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.PasswordMinLength));
                }
            }
        }

        public bool PasswordRequireNonLetterOrDigit
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.PasswordRequireNonLetterOrDigit));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.PasswordRequireNonLetterOrDigit));
                }
            }
        }

        public bool PasswordRequireDigit
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.PasswordRequireDigit));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.PasswordRequireDigit));
                }
            }
        }

        public bool PasswordRequireLowercase
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.PasswordRequireLowercase));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.PasswordRequireLowercase));
                }
            }
        }

        public bool PasswordRequireUppercase
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.PasswordRequireUppercase));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.PasswordRequireUppercase));
                }
            }
        }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.UserLockoutEnabledByDefault));
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.UserLockoutEnabledByDefault));
                }
            }
        }

        public int DefaultAccountLockoutTimeSpan
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.DefaultAccountLockoutTimeSpan));
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.DefaultAccountLockoutTimeSpan));
                }
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                try
                {
                    var val = TryGetValue(nameof(this.MaxFailedAccessAttemptsBeforeLockout));
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException(nameof(this.MaxFailedAccessAttemptsBeforeLockout));
                }
            }
        }

        public string ImageUploadName
        {
            get
            {
                return TryGetValue(nameof(this.ImageUploadName));
            }
        }

        public string ImageUploadApiKey
        {
            get
            {
                return TryGetValue(nameof(this.ImageUploadApiKey));
            }
        }

        public string ImageUploadApiSecret
        {
            get
            {
                return TryGetValue(nameof(this.ImageUploadApiSecret));
            }
        }

        public string MailSenderEmailAddress
        {
            get
            {
                return TryGetValue(nameof(this.MailSenderEmailAddress));
            }
        }

        public string MailUsername
        {
            get
            {
                return TryGetValue(nameof(this.MailUsername));
            }
        }

        public string MailPassword
        {
            get
            {
                return TryGetValue(nameof(this.MailPassword));
            }
        }

        private static string TryGetValue(string settingName)
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
