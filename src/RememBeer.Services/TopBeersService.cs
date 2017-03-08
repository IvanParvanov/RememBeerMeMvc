using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using RememBeer.Common.Cache.Attributes;
using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Models.Dtos;
using RememBeer.Services.Contracts;
using RememBeer.Services.RankingStrategies.Contracts;

namespace RememBeer.Services
{
    public class TopBeersService : ITopBeersService
    {
        private readonly IRepository<BeerReview> reviewsRepository;
        private readonly IRankCalculationStrategy strategy;

        public TopBeersService(IRepository<BeerReview> reviewsRepository, IRankCalculationStrategy strategy)
        {
            if (reviewsRepository == null)
            {
                throw new ArgumentNullException(nameof(reviewsRepository));
            }

            if (strategy == null)
            {
                throw new ArgumentNullException(nameof(strategy));
            }

            this.strategy = strategy;
            this.reviewsRepository = reviewsRepository;
        }

        [Cache(10)]
        public virtual IEnumerable<IBeerRank> GetTopBeers(int top = int.MaxValue)
        {
            var rankings = new List<IBeerRank>();

            var groupedReviews = this.reviewsRepository.All
                                     .Where(r => !r.IsDeleted && !r.Beer.IsDeleted)
                                     .Include(s => s.Beer)
                                     .Include(s => s.Beer.Brewery)
                                     .Include(s => s.User)
                                     .GroupBy(r => r.Beer);

            foreach (var grouping in groupedReviews)
            {
                var rank = this.strategy.GetBeerRank(grouping, grouping.Key);
                rankings.Add(rank);
            }

            return rankings.OrderByDescending(r => r.CompositeScore)
                           .Take(top);
        }

        [Cache(20)]
        public virtual IEnumerable<IBreweryRank> GetTopBreweries(int top)
        {
            var allBeerRankings = this.GetTopBeers();
            var groupedByBrewery = allBeerRankings.GroupBy(b => b.Beer.Brewery.Name)
                                                  .Select(breweryBeers =>
                                                              this.strategy.GetBreweryRank(breweryBeers,
                                                                                           breweryBeers.Key))
                                                  .ToList();


            return groupedByBrewery.OrderByDescending(r => r.AveragePerBeer)
                                   .Take(top);
        }
    }
}
