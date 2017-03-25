using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Areas.Admin.Models
{
    public class CreateBeerBindingModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a beer type from the dropdown.")]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a beer type from the dropdown.")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Beer name")]
        public string BeerName { get; set; }
    }
}
