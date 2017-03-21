using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using RememBeer.Data.Repositories.Base;
using RememBeer.Models;
using RememBeer.Models.Contracts;
using RememBeer.Services.Contracts;

namespace RememBeer.Services
{
    public class BeerTypesService : IBeerTypesService
    {
        private readonly IEfRepository<BeerType> typesRepository;

        public BeerTypesService(IEfRepository<BeerType> typesRepository)
        {
            Guard.WhenArgument(typesRepository, nameof(typesRepository)).IsNull().Throw();

            this.typesRepository = typesRepository;
        }

        public IEnumerable<IBeerType> Search(string name)
        {
            return this.typesRepository.All.Where(t => t.Type.Contains(name)).ToList();
        }
    }
}
