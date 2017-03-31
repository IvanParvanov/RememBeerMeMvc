using System.Web.Mvc;

namespace RememBeer.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return this.View();
        }

        public ViewResult Error()
        {
            return this.View("Error");
        }

        public ViewResult NotFound()
        {
            return this.View("NotFound");
        }
    }
}
