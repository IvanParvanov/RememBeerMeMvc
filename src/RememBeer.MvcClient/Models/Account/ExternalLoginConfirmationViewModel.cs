using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Models.AccountModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}