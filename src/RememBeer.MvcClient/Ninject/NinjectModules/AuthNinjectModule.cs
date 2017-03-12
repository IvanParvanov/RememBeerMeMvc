using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Ninject.Modules;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.MvcClient.Ninject.NinjectModules
{
    [ExcludeFromCodeCoverage]
    public class AuthNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Rebind<IApplicationSignInManager>()
                .ToMethod((context) =>
                          {
                              var cbase = new HttpContextWrapper(HttpContext.Current);
                              var owinCtx = cbase.GetOwinContext();
                              ThrowIfNull(owinCtx);

                              return owinCtx.Get<IApplicationSignInManager>();
                          });

            this.Rebind<IApplicationUserManager>()
                .ToMethod((context) =>
                          {
                              var cbase = new HttpContextWrapper(HttpContext.Current);
                              var owinCtx = cbase.GetOwinContext();
                              ThrowIfNull(owinCtx);

                              return owinCtx.Get<IApplicationUserManager>();
                          });

            this.Rebind<IAuthenticationManager>()
                .ToMethod(context =>
                          {
                              var cbase = new HttpContextWrapper(HttpContext.Current);
                              var owinCtx = cbase.GetOwinContext();
                              ThrowIfNull(owinCtx);

                              return owinCtx.Authentication;
                          });
        }

        private static void ThrowIfNull(object owinCtx)
        {
            if (owinCtx == null)
            {
                throw new ArgumentNullException(nameof(owinCtx));
            }
        }
    }
}
