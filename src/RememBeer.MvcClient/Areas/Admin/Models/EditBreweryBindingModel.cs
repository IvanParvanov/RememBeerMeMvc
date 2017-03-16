using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Areas.Admin.Models
{
    public class EditBreweryBindingModel
    {
        public int Id { get; set; }

        [Display(Name = "Brewery name")]
        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [Required]
        [MaxLength(128)]
        public string Country { get; set; }
    }
}
