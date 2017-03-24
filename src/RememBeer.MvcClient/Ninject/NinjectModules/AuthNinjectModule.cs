using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
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
                .ToMethod(context => GetOwinContext().Get<IApplicationSignInManager>());

            this.Rebind<IApplicationUserManager>()
                .ToMethod(context => GetOwinContext().Get<IApplicationUserManager>());

            this.Rebind<IAuthenticationManager>()
                .ToMethod(context => GetOwinContext().Authentication);
        }

        private static IOwinContext GetOwinContext()
        {
            var cbase = new HttpContextWrapper(HttpContext.Current);
            var owinCtx = cbase.GetOwinContext();

            if (owinCtx == null)
            {
                throw new ArgumentNullException(nameof(owinCtx));
            }

            return owinCtx;
        }
    }
}
