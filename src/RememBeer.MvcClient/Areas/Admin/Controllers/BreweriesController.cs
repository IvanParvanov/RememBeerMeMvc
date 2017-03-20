using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using RememBeer.Common.Constants;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Areas.Admin.Controllers.Base;
using RememBeer.MvcClient.Areas.Admin.Models;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Areas.Admin.Controllers
{
    public class BreweriesController : AdminController
    {
        private readonly IMapper mapper;
        private readonly IBreweryService breweryService;
        private readonly IBeerTypesService beerTypesService;

        public BreweriesController(IMapper mapper,
                                   IBreweryService breweryService,
                                   IBeerTypesService beerTypesService)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(breweryService, nameof(breweryService)).IsNull().Throw();
            Guard.WhenArgument(beerTypesService, nameof(beerTypesService)).IsNull().Throw();

            this.mapper = mapper;
            this.breweryService = breweryService;
            this.beerTypesService = beerTypesService;
        }

        // GET: Admin/Breweries
        public ActionResult Index(int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            var skip = page * pageSize;
            var breweries = this.breweryService.GetAll(skip, pageSize, x => x.Id, searchPattern);
            var viewModel = new PaginatedViewModel<IBrewery>()
                            {
                                Items = breweries,
                                CurrentPage = page,
                                PageSize = pageSize,
                                TotalCount = searchPattern == null ? this.breweryService.CountAll() : this.breweryService.CountAll(searchPattern)
                            };

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_BreweryList", viewModel);
            }

            return this.View(viewModel);
        }

        // GET: Admin/Breweries/Details/5
        public ActionResult Details(int id)
        {
            var brewery = this.breweryService.GetById(id);

            var viewModel = this.mapper.Map<IBrewery, BreweryDetailsViewModel>(brewery);

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_Details", viewModel);
            }

            return this.View(viewModel);
        }

        // POST: Admin/Breweries/Details/5
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Details(CreateBeerBindingModel model)
        {
            var result = this.breweryService.AddNewBeer(model.Id, model.TypeId, model.BeerName);
            if (result.Successful)
            {
                return this.RedirectToAction("Details", new { id = model.Id });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Beer validation failed");
        }

        // PUT: Admin/Breweries/Details/5
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [HttpPut]
        public ActionResult Details(EditBreweryBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.breweryService.UpdateBrewery(model.Id, model.Name, model.Country, model.Description);
                if (result.Successful)
                {
                    var brewery = this.breweryService.GetById(model.Id);
                    var viewModel = this.mapper.Map<IBrewery, BreweryDetailsViewModel>(brewery);

                    return this.PartialView("_Details", viewModel);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Brewery validation failed");
        }

        // POST: Admin/Breweries/DeleteBeer/5
        [AjaxOnly]
        public HttpStatusCodeResult DeleteBeer(int beerId)
        {
            this.breweryService.DeleteBeer(beerId);

            return new HttpStatusCodeResult(HttpStatusCode.OK, "Beer has been deleted!");
        }

        // GET: Admin/Breweries/Types?name={name}
        [AjaxOnly]
        public JsonResult Types(string name)
        {
            var result = this.beerTypesService.Search(name);

            var dtos = this.mapper.Map<IEnumerable<IBeerType>, IEnumerable<BeerTypeDto>>(result);

            return this.Json(new { data = dtos }, JsonRequestBehavior.AllowGet);
        }
    }
}
