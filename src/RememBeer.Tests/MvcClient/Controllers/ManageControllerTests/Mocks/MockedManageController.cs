using Microsoft.Owin.Security;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks
{
    public class MockedManageController : ManageController
    {
        public MockedManageController(IApplicationUserManager userManager, IApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
            : base(userManager, signInManager, authenticationManager)
        {
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
