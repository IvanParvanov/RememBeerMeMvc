using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.MvcClient.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.AdminRole)]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IBeerReviewService reviewService;
        private readonly IMapper mapper;

        public UsersController(IMapper mapper, IUserService userService, IBeerReviewService reviewService)
        {
            Guard.WhenArgument(userService, nameof(userService)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();

            this.userService = userService;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        // GET: Admin/Users
        public ActionResult Index(int page = 0, int pageSize = 10, string searchPattern = null)
        {
            int totalCount;
            var users = this.userService.PaginatedUsers(page, pageSize, out totalCount, searchPattern);

            return this.View(users);
        }

        // GET: Admin/Users/Reviews/id
        public ActionResult Reviews(string id, int page = 0, int pageSize = 10)
        {
            var skip = page * pageSize;
            var reviews = this.reviewService.GetReviewsForUser(id, skip, pageSize);
            var totalCount = this.reviewService.CountUserReviews(id);

            var mappedReviews = this.mapper.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(reviews);
            foreach (var singleReviewViewModel in mappedReviews)
            {
                singleReviewViewModel.IsEdit = true;
            }

            var viewModel = new PaginatedReviewsViewModel()
                            {
                                Items = mappedReviews,
                                TotalCount = totalCount,
                                CurrentPage = page,
                                PageSize = pageSize
                            };

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_ReviewList", viewModel);
            }

            return this.View("My", viewModel);
        }

        // POST: Admin/Users/EnableUser
        [HttpPost]
        public async Task<ActionResult> EnableUser(string userId, int page = 0, int pageSize = 10, string searchPattern = null)
        {
            var result = await this.userService.EnableUserAsync(userId);
            if (result.Succeeded)
            {
                int totalCount;
                var users = this.userService.PaginatedUsers(page, pageSize, out totalCount, searchPattern);

                return this.PartialView("_UserList", users);
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Join(", ", result.Errors));
        }

        // POST: Admin/Users/DisableUser
        [HttpPost]
        public async Task<ActionResult> DisableUser(string userId, int page = 0, int pageSize = 10, string searchPattern = null)
        {
            var result = await this.userService.DisableUserAsync(userId);
            if (result.Succeeded)
            {
                int totalCount;
                var users = this.userService.PaginatedUsers(page, pageSize, out totalCount, searchPattern);

                return this.PartialView("_UserList", users);
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Join(", ", result.Errors));
        }

        // POST: Admin/Users/MakeAdmin
        [HttpPost]
        public ActionResult MakeAdmin(int page = 0, int pageSize = 10, string searchPattern = null)
        {
            int totalCount;
            var users = this.userService.PaginatedUsers(page, pageSize, out totalCount, searchPattern);

            return this.PartialView("_UserList", users);
        }

        // POST: Admin/Users/RemoveAdmin
        [HttpPost]
        public ActionResult RemoveAdmin(int page = 0, int pageSize = 10, string searchPattern = null)
        {
            int totalCount;
            var users = this.userService.PaginatedUsers(page, pageSize, out totalCount, searchPattern);

            return this.PartialView("_UserList", users);
        }
    }
}
