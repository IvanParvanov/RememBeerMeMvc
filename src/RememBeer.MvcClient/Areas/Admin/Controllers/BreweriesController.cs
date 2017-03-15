using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using RememBeer.Common.Constants;
using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Areas.Admin.Controllers
{
    [ValidateInput(false)]
    public class BreweriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBreweryService breweryService;
        private readonly IBeerTypesService beerTypesService;

        public BreweriesController(IMapper mapper, IBreweryService breweryService, IBeerTypesService beerTypesService)
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

            return this.View(brewery);
        }

        [AjaxOnly]
        // GET: Admin/Breweries/Types?name={name}
        public JsonResult Types(string name)
        {
            var result = this.beerTypesService.Search(name);

            var dtos = this.mapper.Map<IEnumerable<IBeerType>, IEnumerable<BeerTypeDto>>(result);

            return this.Json(new { data = dtos }, JsonRequestBehavior.AllowGet);
        }

        //// GET: Admin/Breweries/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Admin/Breweries/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
