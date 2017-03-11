using System.Web.Mvc;

namespace RememBeer.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Error()
        {
            return this.View("Error");
        }
    }
}