using System;

namespace RememBeer.WebClient.UserControls
{
    public partial class BeerRating : System.Web.UI.UserControl
    {
        public string SelectedValue
        {
            get
            {
                return this.DropDownList.SelectedValue;
            }
            set
            {
                this.DropDownList.SelectedValue = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
