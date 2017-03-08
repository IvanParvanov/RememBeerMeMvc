using System;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Ninject;

using Owin;

using RememBeer.Data.DbContexts.Contracts;
using RememBeer.Models;
using RememBeer.Models.Identity;
using RememBeer.Models.Identity.Contracts;

namespace RememBeer.WebClient
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app, IKernel kernel)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => kernel.Get<IRememBeerMeDbContext>());
            app.CreatePerOwinContext<IApplicationUserManager>(
                                                              (factoryOptions, owinContext) =>
                                                              {
                                                                  var dbContext = kernel.Get<IRememBeerMeDbContext>();
                                                                  return kernel.Get<IIdentityFactory>()
                                                                               .GetApplicationUserManager(factoryOptions,
                                                                                                          owinContext,
                                                                                                          (DbContext) dbContext);
                                                              });

            app.CreatePerOwinContext<IApplicationSignInManager>(kernel.Get<IIdentityFactory>()
                                                                      .GetApplicationSignInManager);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            var onValidateIdentity = SecurityStampValidator
                                                        .OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                                                                                                  validateInterval: TimeSpan.FromMinutes (30),
                                                                                                  regenerateIdentity: (manager, user) =>
                                                                                                                        user.GenerateUserIdentityAsync(manager));

            var cookieAuthProvider = new CookieAuthenticationProvider
                                     {
                                         OnValidateIdentity = onValidateIdentity
                                             
                                     };

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                        {
                                            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                            LoginPath = new PathString("/Account/Login"),
                                            Provider = cookieAuthProvider
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
