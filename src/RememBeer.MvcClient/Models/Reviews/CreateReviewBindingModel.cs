using System.ComponentModel.DataAnnotations;
using System.Web;

using RememBeer.MvcClient.Attributes;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class CreateReviewBindingModel : EditReviewBindingModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a beer from the dropdown.")]
        public int BeerId { get; set; }

        [FileType("jpg,png,gif,jpeg,psd", ErrorMessage = "Upload a valid image file.")]
        public HttpPostedFileBase Image { get; set; }
    }
}
