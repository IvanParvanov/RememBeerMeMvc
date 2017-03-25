using System.ComponentModel.DataAnnotations;

namespace RememBeer.MvcClient.Models.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
