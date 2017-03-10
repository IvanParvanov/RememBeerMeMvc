using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;

using RememBeer.Common.Services.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.Services.Contracts;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBeerReviewService reviewService;
        private readonly IBeerService beerService;
        private readonly IImageUploadService imageUpload;

        public ReviewsController(IMapper mapper, IBeerReviewService reviewService, IBeerService beerService, IImageUploadService imageUpload)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();
            Guard.WhenArgument(beerService, nameof(beerService)).IsNull().Throw();
            Guard.WhenArgument(imageUpload, nameof(imageUpload)).IsNull().Throw();

            this.mapper = mapper;
            this.reviewService = reviewService;
            this.beerService = beerService;
            this.imageUpload = imageUpload;
        }

        // GET: Reviews/My
        public ActionResult My(int page = 0, int pageSize = 5)
        {
            var userId = this.User?.Identity?.GetUserId();

            var skip = page * pageSize;
            var reviews = this.reviewService.GetReviewsForUser(userId, skip, pageSize);
            var totalCount = this.reviewService.CountUserReviews(userId);

            var mappedReviews = this.mapper.Map<IEnumerable<IBeerReview>, IEnumerable<SingleReviewViewModel>>(reviews);
            foreach (var singleReviewViewModel in mappedReviews)
            {
                singleReviewViewModel.IsEdit = true;
            }

            var viewModel = new PaginatedReviewsViewModel()
                            {
                                Page = page,
                                Reviews = mappedReviews,
                                TotalCount = totalCount,
                                CurrentPage = page,
                                PageSize = pageSize
                            };

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
        [ValidateAntiForgeryToken]
        [HttpPut]
        public ActionResult Index(EditReviewBindingModel m)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Review validation failed");
            }

            var review = this.reviewService.GetById(m.Id);
            var userId = this.User.Identity.GetUserId();
            if (userId != review.ApplicationUserId && !this.User.IsInRole(Constants.AdminRole))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You cannot edit other people's reviews!");
            }

            this.mapper.Map(m, review);

            var result = this.reviewService.UpdateReview(review);
            if (!result.Successful)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Review validation failed");
            }

            var mapped = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);
            mapped.IsEdit = true;

            return this.PartialView("Partial/_SingleReview", mapped);
        }

        // POST: Reviews/ChangeImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeImage(ChangeImageBindingModel model)
        {
            var img = model.Image;
            var id = model.Id;
            var imgArray = this.StreamToArray(img.InputStream);

            var url = this.imageUpload.UploadImage(imgArray, Constants.DefaultThumbnailSizePx, Constants.DefaultThumbnailSizePx);
            if (url == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Image could not be uploaded.");
            }

            var review = this.reviewService.GetById(id);
            var userId = this.User.Identity.GetUserId();
            if (userId != review.ApplicationUserId && !this.User.IsInRole(Constants.AdminRole))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You cannot edit other people's reviews!");
            }

            review.ImgUrl = url;
            var result = this.reviewService.UpdateReview(review);
            if (result.Successful)
            {
                return this.Json(new { url });
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Image could not be uploaded.");
        }

        private byte[] StreamToArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
