using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class ChangeImageBindingModel
    {
        public int Id { get; set; }

        [Required]
        [FileExtensions(ErrorMessage = "Upload a valid image file.")]
        public HttpPostedFileBase Image { get; set; }
    }
}