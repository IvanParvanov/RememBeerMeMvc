using System.Data.Entity;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models.Identity
{
    public interface IIdentityFactory
    {
        IApplicationUserManager GetApplicationUserManager(IdentityFactoryOptions<IApplicationUserManager> options,
                                                          IOwinContext context,
                                                          DbContext dbContext);

        IApplicationSignInManager GetApplicationSignInManager(IdentityFactoryOptions<IApplicationSignInManager> options,
                                                              IOwinContext context);
    }
}
