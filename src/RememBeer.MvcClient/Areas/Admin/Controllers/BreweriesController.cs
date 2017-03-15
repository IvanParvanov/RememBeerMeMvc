using System.Web.Mvc;

using Bytes2you.Validation;

using RememBeer.Common.Constants;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Areas.Admin.Controllers
{
    [ValidateInput(false)]
    public class BreweriesController : Controller
    {
        private readonly IBreweryService breweryService;

        public BreweriesController(IBreweryService breweryService)
        {
            Guard.WhenArgument(breweryService, nameof(breweryService)).IsNull().Throw();

            this.breweryService = breweryService;
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
