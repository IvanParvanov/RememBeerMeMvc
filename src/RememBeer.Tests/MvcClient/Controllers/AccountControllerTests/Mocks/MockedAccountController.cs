using Microsoft.Owin.Security;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;

namespace RememBeer.Tests.MvcClient.Controllers.AccountControllerTests.Mocks
{
    public class MockedAccountController : AccountController
    {
        public MockedAccountController(IApplicationUserManager userManager, IApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
            : base(userManager, signInManager, authenticationManager)
        {
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
