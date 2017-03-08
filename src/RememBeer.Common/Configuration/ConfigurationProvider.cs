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
                    var val = TryGetValue("UserNamesAllowOnlyAlphanumeric");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("UserNamesAllowOnlyAlphanumeric");
                }
            }
        }

        public bool RequireUniqueEmail
        {
            get
            {
                try
                {
                    var val = TryGetValue("RequireUniqueEmail");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("RequireUniqueEmail");
                }
            }
        }

        public int PasswordMinLength
        {
            get
            {
                try
                {
                    var val = TryGetValue("PasswordMinLength");
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("PasswordMinLength");
                }
            }
        }

        public bool PasswordRequireNonLetterOrDigit
        {
            get
            {
                try
                {
                    var val = TryGetValue("PasswordRequireNonLetterOrDigit");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("PasswordRequireNonLetterOrDigit");
                }
            }
        }

        public bool PasswordRequireDigit
        {
            get
            {
                try
                {
                    var val = TryGetValue("PasswordRequireDigit");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("PasswordRequireDigit");
                }
            }
        }

        public bool PasswordRequireLowercase
        {
            get
            {
                try
                {
                    var val = TryGetValue("PasswordRequireLowercase");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("PasswordRequireLowercase");
                }
            }
        }

        public bool PasswordRequireUppercase
        {
            get
            {
                try
                {
                    var val = TryGetValue("PasswordRequireUppercase");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("PasswordRequireUppercase");
                }
            }
        }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                try
                {
                    var val = TryGetValue("UserLockoutEnabledByDefault");
                    return bool.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("UserLockoutEnabledByDefault");
                }
            }
        }

        public int DefaultAccountLockoutTimeSpan
        {
            get
            {
                try
                {
                    var val = TryGetValue("DefaultAccountLockoutTimeSpan");
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("DefaultAccountLockoutTimeSpan");
                }
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                try
                {
                    var val = TryGetValue("MaxFailedAccessAttemptsBeforeLockout");
                    return int.Parse(val);
                }
                catch (FormatException)
                {
                    throw new InvalidConfigurationOptionException("MaxFailedAccessAttemptsBeforeLockout");
                }
            }
        }

        public string ImageUploadName
        {
            get
            {
                return TryGetValue("ImageUploadName");
            }
        }

        public string ImageUploadApiKey
        {
            get
            {
                return TryGetValue("ImageUploadApiKey");
            }
        }

        public string ImageUploadApiSecret
        {
            get
            {
                return TryGetValue("ImageUploadApiSecret");
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
