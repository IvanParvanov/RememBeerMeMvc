using System;
using System.Web.UI.WebControls;

using RememBeer.Business.Logic.Admin.Breweries;
using RememBeer.Business.Logic.Admin.Breweries.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Admin
{
    [PresenterBinding(typeof(BreweriesPresenter))]
    public partial class Admin : BaseMvpPage<BreweriesViewModel>, IBreweriesView
    {
        public event EventHandler<EventArgs> Initialized;

        public event EventHandler<ISearchEventArgs> BrewerySearch;

        protected void Page_Load(object sender, EventArgs e)
        {
            var pattern = this.Request.QueryString["s"];
            if (pattern != null)
            {
                var args = this.EventArgsFactory.CreateSearchEventArgs(pattern);
                this.BrewerySearch?.Invoke(this, args);
            }
            else
            {
                this.Initialized?.Invoke(this, EventArgs.Empty);
            }

            this.BindData();
        }

        protected void GridView1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                this.GridView1.PageIndex = e.NewPageIndex;
                this.BindData();
            }
        }

        protected void Search_OnClick(object sender, EventArgs e)
        {
            var pattern = this.SearchTb.Text;
            this.Response.Redirect("Default.aspx?s=" + pattern);
        }

        private void BindData()
        {
            this.GridView1.DataSource = this.Model.Breweries;
            this.GridView1.DataBind();
        }
    }
}
