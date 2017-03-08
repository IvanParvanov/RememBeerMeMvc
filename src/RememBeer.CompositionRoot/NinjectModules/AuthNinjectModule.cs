using System;
using System.Web;

using Microsoft.AspNet.Identity.Owin;

using Ninject.Modules;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.CompositionRoot.NinjectModules
{
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
