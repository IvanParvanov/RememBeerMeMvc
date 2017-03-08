using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Breweries;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace RememBeer.WebClient.Top
{
    [PresenterBinding(typeof(TopBreweriesPresenter))]
    public partial class Breweries : MvpPage<TopBreweriesViewModel>, IInitializableView<TopBreweriesViewModel>
    {
        public event EventHandler<EventArgs> Initialized;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.Initialized?.Invoke(this, EventArgs.Empty);
            this.RankingGridView.DataSource = this.Model.Rankings;
            this.RankingGridView.DataBind();
        }
    }
}
