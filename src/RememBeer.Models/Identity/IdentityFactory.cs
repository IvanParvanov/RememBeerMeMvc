using System;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

using RememBeer.Common.Configuration;
using RememBeer.Common.Services;
using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models.Identity
{
    public class IdentityFactory : IIdentityFactory
    {
        private readonly IConfigurationProvider config;

        public IdentityFactory(IConfigurationProvider configProvider)
        {
            if (configProvider == null)
            {
                throw new ArgumentNullException(nameof(configProvider));
            }

            this.config = configProvider;
        }

        public IApplicationUserManager GetApplicationUserManager(
            IdentityFactoryOptions<IApplicationUserManager> options,
            IOwinContext context,
            DbContext dbContext)
        {
            var manager =
                new ApplicationUserManager(
                                           new UserStore<ApplicationUser>(dbContext));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                                    {
                                        AllowOnlyAlphanumericUserNames = this.config.UserNamesAllowOnlyAlphanumeric,
                                        RequireUniqueEmail = this.config.RequireUniqueEmail
                                    };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
                                        {
                                            RequiredLength = this.config.PasswordMinLength,
                                            RequireNonLetterOrDigit = this.config.PasswordRequireNonLetterOrDigit,
                                            RequireDigit = this.config.PasswordRequireDigit,
                                            RequireLowercase = this.config.PasswordRequireLowercase,
                                            RequireUppercase = this.config.PasswordRequireUppercase,
                                        };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            //                                                {
            //                                                    Subject = "Security Code",
            //                                                    BodyFormat = "Your security code is {0}"
            //                                                });
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = this.config.UserLockoutEnabledByDefault;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(this.config.DefaultAccountLockoutTimeSpan);
            manager.MaxFailedAccessAttemptsBeforeLockout = this.config.MaxFailedAccessAttemptsBeforeLockout;

            manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public IApplicationSignInManager GetApplicationSignInManager(
            IdentityFactoryOptions<IApplicationSignInManager> options,
            IOwinContext context)
        {
            return
                new ApplicationSignInManager((ApplicationUserManager)context.GetUserManager<IApplicationUserManager>(),
                                             context.Authentication);
        }
    }
}
