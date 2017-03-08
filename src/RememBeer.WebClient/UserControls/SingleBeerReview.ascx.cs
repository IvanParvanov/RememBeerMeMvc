using System;

using RememBeer.Models.Contracts;

namespace RememBeer.WebClient.UserControls
{
    public partial class SingleBeerReview : System.Web.UI.UserControl
    {
        private IBeerReview beerReview;

        public bool IsEdit
        {
            get
            {
                return this.EditButton.Visible;
            }
            set
            {
                this.EditPlaceholder.Visible = value;
            }
        }

        public IBeerReview Review
        {
            get
            {
                return this.beerReview;
            }
            set
            {
                this.beerReview = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
