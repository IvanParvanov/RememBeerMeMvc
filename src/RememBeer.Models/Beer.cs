using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using RememBeer.Models.Contracts;

namespace RememBeer.Models
{
    public class Beer : Identifiable, IBeer
    {
        public Beer()
        {
            this.Reviews = new HashSet<BeerReview>();
        }

        public virtual BeerType BeerType { get; set; }

        public int BeerTypeId { get; set; }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        public virtual Brewery Brewery { get; set; }

        public int BreweryId { get; set; }

        public virtual ICollection<BeerReview> Reviews { get; set; }

        public bool IsDeleted { get; set; }
    }
}
