using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Controllers
{
    public class TopController : Controller
    {
        private readonly ITopBeersService topService;

        public TopController(ITopBeersService topService)
        {
            Guard.WhenArgument(topService, nameof(topService)).IsNull().Throw();

            this.topService = topService;
        }

        // GET: Top/TopBeers
        public ActionResult TopBeers()
        {
            var topBeers = this.topService.GetTopBeers(10).ToList();
            return this.View(topBeers);
        }

        // GET: Top/TopBeers
        public ActionResult TopBreweries()
        {
            var topBreweries = this.topService.GetTopBreweries(10).ToList();
            return this.View(topBreweries);
        }
    }
}