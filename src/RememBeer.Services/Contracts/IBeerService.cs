using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IBeerService
    {
        IEnumerable<IBeer> SearchBeers(string name);

        IBeer Get(int id);
    }
}