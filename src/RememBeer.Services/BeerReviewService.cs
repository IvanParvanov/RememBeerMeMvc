using System;
using System.Collections.Generic;
using System.Linq;

using RememBeer.Data.Repositories;
using RememBeer.Data.Repositories.Base;
using RememBeer.Data.Repositories.Enums;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class BeerReviewService : IBeerReviewService
    {
        private readonly IEfRepository<BeerReview> repository;

        public BeerReviewService(IEfRepository<BeerReview> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public IEnumerable<IBeerReview> GetReviewsForUser(string userId)
        {
            return this.repository.GetAll(x => x.IsDeleted == false && x.ApplicationUserId == userId,
                                          x => x.CreatedAt,
                                          SortOrder.Descending);
        }

        public IEnumerable<IBeerReview> GetReviewsForUser(string userId, int skip, int pageSize, string searchPattern = null)
        {
            var result = this.repository.All;
            if (string.IsNullOrEmpty(searchPattern))
            {
                result = result.Where(x => x.IsDeleted == false && x.ApplicationUserId == userId);
            }
            else
            {
                result = result.Where(x => x.IsDeleted == false && x.ApplicationUserId == userId && (x.Beer.Name.Contains(searchPattern) || x.Beer.Brewery.Name.Contains(searchPattern) || x.Place.Contains(searchPattern) ));
            }

            return result.OrderByDescending(x => x.CreatedAt)
                         .Skip(skip)
                         .Take(pageSize)
                         .ToList();
        }

        public int CountUserReviews(string userId, string searchPattern = null)
        {
            if (string.IsNullOrEmpty(searchPattern))
            {
                return this.repository.All.Count(x => x.ApplicationUserId == userId && x.IsDeleted == false);
            }

            return this.repository.All.Count(x => x.ApplicationUserId == userId
                                                  && x.IsDeleted == false
                                                  && (x.Beer.Name.Contains(searchPattern)
                                                      || x.Beer.Brewery.Name.Contains(searchPattern)
                                                      || x.Place.Contains(searchPattern)));
        }

        public IDataModifiedResult UpdateReview(IBeerReview review)
        {
            var rv = review as BeerReview;
            this.repository.Update(rv);
            return this.repository.SaveChanges();
        }

        public IDataModifiedResult CreateReview(IBeerReview review)
        {
            var rv = review as BeerReview;
            this.repository.Add(rv);
            return this.repository.SaveChanges();
        }

        public IDataModifiedResult DeleteReview(object id)
        {
            var review = this.repository.GetById(id);
            review.IsDeleted = true;
            return this.repository.SaveChanges();
        }

        public IBeerReview GetById(object id)
        {
            return this.repository.GetById(id);
        }

        public IBeerReview GetLatestForUser(string userId)
        {
            return this.repository.All.Where(r => r.ApplicationUserId == userId).OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        }
    }
}
