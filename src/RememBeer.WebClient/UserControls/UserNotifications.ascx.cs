using System;
using System.Web.UI;

namespace RememBeer.WebClient.UserControls
{
    public partial class UserNotifications : UserControl
    {
        public bool SuccessVisible
        {
            get
            {
                return this.SuccessMessagePlaceholder.Visible;
            }
            set
            {
                this.SuccessMessagePlaceholder.Visible = value;
            }
        }

        public string SuccessText
        {
            get
            {
                return this.SuccessMessage.Text;
            }
            set
            {
                this.SuccessMessagePlaceholder.Visible = true;
                this.SuccessMessage.Text = value;
            }
        }

        public bool WarningVisible
        {
            get
            {
                return this.WarningMessagePlaceholder.Visible;
            }
            set
            {
                this.WarningMessagePlaceholder.Visible = value;
            }
        }

        public string WarningText
        {
            get
            {
                return this.WarningMessage.Text;
            }
            set
            {
                this.WarningMessagePlaceholder.Visible = true;
                this.WarningMessage.Text = value;
            }
        }

        public bool ErrorVisible
        {
            get
            {
                return this.ErrorMessagePlaceholder.Visible;
            }
            set
            {
                this.ErrorMessagePlaceholder.Visible = value;
            }
        }

        public string ErrorText
        {
            get
            {
                return this.ErrorMessage.Text;
            }
            set
            {
                this.ErrorMessagePlaceholder.Visible = true;
                this.ErrorMessage.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
