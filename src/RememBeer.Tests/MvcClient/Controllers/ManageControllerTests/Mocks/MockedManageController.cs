using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient.Controllers;
using RememBeer.Services.Contracts;

namespace RememBeer.Tests.MvcClient.Controllers.ManageControllerTests.Mocks
{
    public class MockedManageController : ManageController
    {
        public MockedManageController(IApplicationUserManager userManager,
                                      IApplicationSignInManager signInManager,
                                      IFollowerService followerService)
            : base(userManager, signInManager, followerService)
        {
        }

        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
