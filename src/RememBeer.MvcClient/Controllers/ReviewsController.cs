using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;

using RememBeer.Common.Services.Contracts;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.MvcClient.Filters;
using RememBeer.MvcClient.Models.Reviews;
using RememBeer.MvcClient.Models.Shared;
using RememBeer.Services.Contracts;

using Constants = RememBeer.Common.Constants.Constants;

namespace RememBeer.MvcClient.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBeerReviewService reviewService;
        private readonly IImageUploadService imageUpload;

        public ReviewsController(IMapper mapper, IBeerReviewService reviewService, IImageUploadService imageUpload)
        {
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();
            Guard.WhenArgument(imageUpload, nameof(imageUpload)).IsNull().Throw();

            this.mapper = mapper;
            this.reviewService = reviewService;
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
                                Items = mappedReviews,
                                TotalCount = totalCount,
                                CurrentPage = page,
                                PageSize = pageSize
                            };

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_ReviewList", viewModel);
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
        [AjaxOnly]
        public ActionResult Edit(int id)
        {
            var review = this.reviewService.GetById(id);
            var mapped = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);

            return this.PartialView("_Edit", mapped);
        }

        // PUT: Reviews
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HttpPut]
        public ActionResult Index(EditReviewBindingModel m)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ReviewValidationFailure();
            }

            var review = this.reviewService.GetById(m.Id);
            var userId = this.User?.Identity?.GetUserId();
            if (userId != review.ApplicationUserId && !this.User.IsInRole(Constants.AdminRole))
            {
                return this.Unauthorized();
            }

            this.mapper.Map(m, review);

            var result = this.reviewService.UpdateReview(review);
            if (!result.Successful)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(", ", result.Errors));
            }

            var mapped = this.mapper.Map<IBeerReview, SingleReviewViewModel>(review);
            mapped.IsEdit = true;

            return this.PartialView("_SingleReview", mapped);
        }

        // DELETE: Reviews
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HttpDelete]
        public HttpStatusCodeResult Index(int id)
        {
            var review = this.reviewService.GetById(id);
            var userId = this.User.Identity.GetUserId();
            if (userId != review.ApplicationUserId && !this.User.IsInRole(Constants.AdminRole))
            {
                return this.Unauthorized();
            }

            this.reviewService.DeleteReview(id);

            return new HttpStatusCodeResult(HttpStatusCode.OK, "Review has been deleted successfully!");
        }

        // POST: Reviews/ChangeImage
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeImage(ChangeImageBindingModel model)
        {
            var img = model.Image;
            var id = model.Id;
            var imgArray = this.StreamToArray(img.InputStream);

            var review = this.reviewService.GetById(id);
            var userId = this.User?.Identity.GetUserId();
            if (userId != review.ApplicationUserId && !this.User.IsInRole(Constants.AdminRole))
            {
                return this.Unauthorized();
            }

            var url = this.imageUpload.UploadImage(imgArray, Constants.DefaultImageSizePx, Constants.DefaultImageSizePx);
            if (url == null)
            {
                return this.ImageUploadFailure();
            }

            review.ImgUrl = url;
            var result = this.reviewService.UpdateReview(review);
            if (result.Successful)
            {
                return this.Json(new UrlOnlyDto { url = url });
            }

            return this.ImageUploadFailure();
        }

        // POST: Reviews/Index
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CreateReviewBindingModel m)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ReviewValidationFailure();
            }

            string imgUrl = null;
            if (m.Image != null)
            {
                var stream = m.Image.InputStream;
                var image = this.StreamToArray(stream);
                imgUrl = this.imageUpload.UploadImage(image, Constants.DefaultImageSizePx, Constants.DefaultImageSizePx);
                if (imgUrl == null)
                {
                    return this.ImageUploadFailure();
                }
            }

            var review = new BeerReview();
            review = this.mapper.Map(m, review);
            review.ApplicationUserId = this.User.Identity.GetUserId();
            review.ImgUrl = imgUrl ?? review.ImgUrl;

            var result = this.reviewService.CreateReview(review);
            if (result.Successful)
            {
                return this.RedirectToAction("My");
            }

            return this.ReviewValidationFailure();
        }

        private HttpStatusCodeResult ImageUploadFailure()
        {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Image could not be uploaded.");
        }

        private HttpStatusCodeResult ReviewValidationFailure()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Review validation failed");
        }

        private HttpStatusCodeResult Unauthorized()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You cannot edit other people's reviews!");
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
