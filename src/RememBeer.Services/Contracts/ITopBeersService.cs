using System.Collections.Generic;

using RememBeer.Models.Dtos;

namespace RememBeer.Services.Contracts
{
    public interface ITopBeersService
    {
        IEnumerable<IBeerRank> GetTopBeers(int top);

        IEnumerable<IBreweryRank> GetTopBreweries(int top);
    }
}
