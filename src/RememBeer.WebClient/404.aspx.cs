using System;
using System.Web.UI;

namespace RememBeer.WebClient
{
    public partial class _404 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = 404;
            this.Response.StatusDescription = "Page not found";
            this.Response.Flush();
        }
    }
}
