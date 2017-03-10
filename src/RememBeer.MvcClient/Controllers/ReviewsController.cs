using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IBeerReviewService reviewService;
        private readonly IBeerService beerService;
        private readonly IMapper mapper;

        public ReviewsController(IMapper mapper, IBeerReviewService reviewService, IBeerService beerService)
        {
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(beerService, nameof(beerService)).IsNull().Throw();

            this.reviewService = reviewService;
            this.beerService = beerService;
            this.mapper = mapper;
        }

        // GET: Reviews/My
        public ActionResult My(int page = 0, int pageSize = 5)
        {
            var userId = this.User?.Identity?.GetUserId();

            var skip = page * pageSize;
            var reviews = this.reviewService.GetReviewsForUser(userId, skip, pageSize);
            var totalCount = this.reviewService.CountUserReviews(userId);

            var mappedReviews = this.mapper.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(reviews, opts: options => options.AfterMap((s, d) =>
                                                                                                                                                         {
                                                                                                                                                             foreach (var model in d)
                                                                                                                                                             {
                                                                                                                                                                 model.IsEdit = true;
                                                                                                                                                             }
                                                                                                                                                         }));
            var viewModel = new PaginatedReviewsViewModel() { Page = page, Reviews = mappedReviews, TotalCount = totalCount, CurrentPage = page, PageSize = pageSize };

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("Partial/_ReviewList", viewModel);
            }

            return this.View(viewModel);
        }

        // GET: Reviews/Details/{id}
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            var review = this.reviewService.GetById(id);
            if (review == null)
            {
                return this.View("NotFound");
            }

            var mappedReview = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);

            return this.View(mappedReview);
        }

        // GET: Reviews/Edit/{id}
        public ActionResult Edit(int id)
        {
            var review = this.reviewService.GetById(id);
            var mapped = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);

            return this.PartialView("Partial/_Edit", mapped);

        }

        // PUT: Reviews
        [HttpPut]
        public ActionResult Index(EditReviewBindingModel m)
        {
            var review = this.reviewService.GetById(m.Id);
            this.mapper.Map(m, review);

            var result = this.reviewService.UpdateReview(review);
            if (result.Successful)
            {
                var mapped = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);
                mapped.IsEdit = true;

                return this.PartialView("Partial/_SingleReview", mapped);
            }

            return this.View("NotFound");
        }
    }
}
