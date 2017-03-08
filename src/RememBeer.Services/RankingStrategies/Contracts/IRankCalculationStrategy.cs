using System.Collections.Generic;

using RememBeer.Models.Contracts;
using RememBeer.Models.Dtos;

namespace RememBeer.Services.RankingStrategies.Contracts
{
    public interface IRankCalculationStrategy
    {
        IBeerRank GetBeerRank(IEnumerable<IBeerReview> reviews, IBeer beer);

        IBreweryRank GetBreweryRank(IEnumerable<IBeerRank> beerRanks, string breweryName);
    }
}
