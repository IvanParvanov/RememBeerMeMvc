using System.Web.Mvc;

namespace RememBeer.MvcClient.Controllers
{
    [ValidateInput(false)]
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
    }
}