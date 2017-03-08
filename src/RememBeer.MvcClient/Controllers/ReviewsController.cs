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
    public class ReviewsController : Controller
    {
        private readonly IBeerReviewService reviewService;
        private readonly IMapper mapper;

        public ReviewsController(IBeerReviewService reviewService, IMapper mapper)
        {
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();

            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        // GET: Reviews/My
        [Authorize]
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
            return this.View(viewModel);
        }

        // GET: Reviews/{id}
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
    }
}
