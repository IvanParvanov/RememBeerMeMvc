using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Models.AccountModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}