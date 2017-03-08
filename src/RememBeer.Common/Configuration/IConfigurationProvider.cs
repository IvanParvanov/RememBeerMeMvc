namespace RememBeer.Common.Configuration
{
    public interface IConfigurationProvider
    {
        bool UserNamesAllowOnlyAlphanumeric { get; }

        bool RequireUniqueEmail { get; }

        int PasswordMinLength { get; }

        bool PasswordRequireNonLetterOrDigit { get; }

        bool PasswordRequireDigit { get; }

        bool PasswordRequireLowercase { get; }

        bool PasswordRequireUppercase { get; }

        bool UserLockoutEnabledByDefault { get; }

        int DefaultAccountLockoutTimeSpan { get; }

        int MaxFailedAccessAttemptsBeforeLockout { get; }

        string ImageUploadName { get; }

        string ImageUploadApiKey { get; }

        string ImageUploadApiSecret { get; }
    }
}
