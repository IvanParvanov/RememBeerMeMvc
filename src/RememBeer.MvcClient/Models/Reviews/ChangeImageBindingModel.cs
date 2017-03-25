using System.ComponentModel.DataAnnotations;
using System.Web;

using RememBeer.MvcClient.Attributes;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class ChangeImageBindingModel
    {
        public int Id { get; set; }

        [Required]
        [FileType("jpg,png,gif,jpeg,psd", ErrorMessage = "Upload a valid image file.")]
        public HttpPostedFileBase Image { get; set; }
    }
}
