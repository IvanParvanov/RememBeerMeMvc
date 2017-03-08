using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using RememBeer.Models.Contracts;

namespace RememBeer.Models
{
    public class Brewery : Identifiable, IBrewery
    {
        private ICollection<Beer> beers;

        public Brewery()
        {
            this.beers = new HashSet<Beer>();
        }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [Required]
        [MaxLength(128)]
        public string Country { get; set; }

        public virtual ICollection<Beer> Beers
        {
            get
            {
                return this.beers;
            }

            set
            {
                this.beers = value;
            }
        }
    }
}
