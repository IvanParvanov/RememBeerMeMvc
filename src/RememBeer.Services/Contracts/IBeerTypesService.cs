using System.Collections.Generic;

using RememBeer.Models.Contracts;

namespace RememBeer.Services.Contracts
{
    public interface IBeerTypesService
    {
        IEnumerable<IBeerType> Search(string name);
    }
}