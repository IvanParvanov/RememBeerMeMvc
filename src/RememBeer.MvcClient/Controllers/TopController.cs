using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using RememBeer.Common.Constants;
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
        public ViewResult TopBeers()
        {
            var topBeers = this.topService.GetTopBeers(Constants.TopBeersCount).ToList();
            return this.View(topBeers);
        }

        // GET: Top/TopBeers
        public ViewResult TopBreweries()
        {
            var topBreweries = this.topService.GetTopBreweries(Constants.TopBeersCount).ToList();
            return this.View(topBreweries);
        }
    }
}