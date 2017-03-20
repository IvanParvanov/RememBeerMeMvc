using System.Web.Mvc;

namespace RememBeer.Tests.Utils.TestExtensions
{
    public static class ControllerExtensions
    {
        public static void InvalidateViewModel<TController>(this TController controller)
            where TController : Controller
        {
            controller.ModelState.AddModelError("", "ERROR");
        }
    }
}
