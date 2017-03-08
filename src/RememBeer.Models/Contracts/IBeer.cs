using System.Collections.Generic;

namespace RememBeer.Models.Contracts
{
    public interface IBeer : IIdentifiable
    {
        BeerType BeerType { get; set; }

        Brewery Brewery { get; set; }

        ICollection<BeerReview> Reviews { get; set; }

        int BeerTypeId { get; set; }

        int BreweryId { get; set; }

        string Name { get; set; }

        bool IsDeleted { get; set; }
    }
}
