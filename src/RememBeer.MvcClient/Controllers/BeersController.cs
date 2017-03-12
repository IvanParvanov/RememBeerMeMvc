using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Filters;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeerService beerService;
        private readonly IMapper mapper;

        public BeersController(IMapper mapper, IBeerService beerService)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(beerService, nameof(beerService)).IsNull().Throw();

            this.mapper = mapper;
            this.beerService = beerService;
        }

        // GET: Beers?name={name}
        [AjaxOnly]
        public ActionResult Index(string name)
        {
            var beers = this.beerService.SearchBeers(name);

            var dtos = this.mapper.Map<IEnumerable<IBeer>, IEnumerable<BeerDto>>(beers);

            return this.Json(dtos, JsonRequestBehavior.AllowGet);
        }
    }
}
