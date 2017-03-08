using System.Configuration;

namespace RememBeer.Common.Exceptions
{
    public class InvalidConfigurationOptionException : ConfigurationErrorsException
    {
        private const string MessageFormat =
            "The configuration options setting \"{0}\" is invalid. Fix it in the application configuration file.";

        /// <summary>Initializes a new instance of the <see cref="T:RememBeer.Common.Exceptions.InvalidConfigurationOptionException" /> class.</summary>
        public InvalidConfigurationOptionException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:RememBeer.Common.Exceptions.InvalidConfigurationOptionException" /> class with a specified setting name.</summary>
        /// <param name="settingName">The name of the invalid configuration option.</param>
        public InvalidConfigurationOptionException(string settingName)
            : base(string.Format(MessageFormat, settingName))
        {
        }
    }
}
