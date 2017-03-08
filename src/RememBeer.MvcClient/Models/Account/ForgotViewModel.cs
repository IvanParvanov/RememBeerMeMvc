using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Models.AccountModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}