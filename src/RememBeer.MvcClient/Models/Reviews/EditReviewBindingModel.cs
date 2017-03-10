using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace RememBeer.MvcClient.Models.Reviews
{
    public class EditReviewBindingModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Location")]
        public string Place { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int Overall { get; set; }

        [Range(1, 10)]
        public int Taste { get; set; }

        [Range(1, 10)]
        [DisplayName("Aroma")]
        public int Smell { get; set; }

        [Range(1, 10)]
        [DisplayName("Looks")]
        public int Look { get; set; }

        public static IList<SelectListItem> ScoreValues
        {
            get
            {
                var items = new List<SelectListItem>();
                for (int i = 1; i < 11; i++)
                {
                    var value = i.ToString();
                    items.Add(new SelectListItem() {Value = value, Text = value});
                }

                return items;
            }
        }
    }
}
