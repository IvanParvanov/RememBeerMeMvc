using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Bytes2you.Validation;

using RememBeer.Common.Constants;
using RememBeer.Models.Dtos;
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
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 600)]
        public PartialViewResult Beers()
        {
            var topBeers = this.topService.GetTopBeers(Constants.TopBeersCount).ToList();
            return this.PartialView("Partial/_Beers", topBeers);
        }

        // GET: Top/TopBeers
        public ViewResult TopBreweries()
        {
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 1200)]
        public PartialViewResult Breweries()
        {
            var topBreweries = this.topService.GetTopBreweries(Constants.TopBeersCount).ToList();
            return this.PartialView("Partial/_Breweries", topBreweries);
        }
    }
}