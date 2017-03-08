using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using RememBeer.Models.Contracts;

namespace RememBeer.Models
{
    public class BeerType : Identifiable, IBeerType
    {
        public BeerType()
        {
            this.Beers = new HashSet<Beer>();
        }

        [Required]
        [MaxLength(512)]
        public string Type { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
    }
}
