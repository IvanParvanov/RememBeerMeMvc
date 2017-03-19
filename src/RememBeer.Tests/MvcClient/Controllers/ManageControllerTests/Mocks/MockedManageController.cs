using Microsoft.Owin.Security;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks
{
    public class MockedManageController : ManageController
    {
        public MockedManageController(IApplicationUserManager userManager,
                                      IApplicationSignInManager signInManager,
                                      IAuthenticationManager authenticationManager,
                                      IFollowerService followerService)
            : base(userManager, signInManager, authenticationManager, followerService)
        {
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
