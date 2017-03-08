using System.Configuration;

namespace RememBeer.Common.Exceptions
{
    public class MissingConfigurationOption : ConfigurationErrorsException
    {
        private const string MessageFormat =
            "The configuration option \"{0}\" is missing from the application configuration file.";

        public MissingConfigurationOption()
        {
        }

        public MissingConfigurationOption(string settingName) 
            : base(string.Format(MessageFormat, settingName))
        {
        }
    }
}
