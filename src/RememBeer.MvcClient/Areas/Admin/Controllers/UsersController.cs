using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Areas.Admin.Controllers.Base;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.MvcClient.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly IBeerReviewService reviewService;
        private readonly IMapper mapper;

        public UsersController(IMapper mapper, IUserService userService, IBeerReviewService reviewService)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(userService, nameof(userService)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();

            this.mapper = mapper;
            this.userService = userService;
            this.reviewService = reviewService;
        }

        // GET: Admin/Users
        public ActionResult Index(int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            page = page < 0 ? 0 : page;
            pageSize = pageSize < 0 ? 1 : pageSize;
            int totalCount = 0;
            var users = this.userService.PaginatedUsers(page, pageSize, ref totalCount, searchPattern);

            var viewModel = new PaginatedViewModel<IApplicationUser>()
                            {
                                Items = users,
                                CurrentPage = page,
                                PageSize = pageSize,
                                TotalCount = totalCount
                            };

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_UserList", viewModel);
            }

            return this.View("Index", viewModel);
        }

        // GET: Admin/Users/Reviews/id
        public ActionResult Reviews(string id, int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            page = page < 0 ? 0 : page;
            pageSize = pageSize < 0 ? 1 : pageSize;
            var skip = page * pageSize;
            var reviews = this.reviewService.GetReviewsForUser(id, skip, pageSize, searchPattern);
            var totalCount = this.reviewService.CountUserReviews(id, searchPattern);

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
        public async Task<ActionResult> EnableUser(string userId, int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            await this.userService.EnableUserAsync(userId);

            return this.RedirectToIndex(page, pageSize, searchPattern);
        }

        // POST: Admin/Users/DisableUser
        [HttpPost]
        public async Task<ActionResult> DisableUser(string userId, int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            await this.userService.DisableUserAsync(userId);

            return this.RedirectToIndex(page, pageSize, searchPattern);
        }

        // POST: Admin/Users/MakeAdmin
        [HttpPost]
        public async Task<ActionResult> MakeAdmin(string userId, int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            await this.userService.MakeAdminAsync(userId);

            return this.RedirectToIndex(page, pageSize, searchPattern);
        }

        // POST: Admin/Users/RemoveAdmin
        [HttpPost]
        public async Task<ActionResult> RemoveAdmin(string userId, int page = 0, int pageSize = Constants.DefaultPageSize, string searchPattern = null)
        {
            await this.userService.RemoveAdminAsync(userId);

            return this.RedirectToIndex(page, pageSize, searchPattern);
        }

        private RedirectToRouteResult RedirectToIndex(int page, int pageSize, string searchPattern)
        {
            return this.RedirectToAction("Index", new { page = page, pageSize = pageSize, searchPattern = searchPattern });
        }
    }
}
