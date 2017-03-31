using System.Web.Mvc;

using RememBeer.Common.Constants;

namespace RememBeer.MvcClient.Areas.Admin.Controllers.Base
{
    [Authorize(Roles = Constants.AdminRole)]
    [ValidateInput(false)]
    public abstract class AdminController : Controller
    {
    }
}
