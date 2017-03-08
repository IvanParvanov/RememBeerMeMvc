using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Beers;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace RememBeer.WebClient.Top
{
    [PresenterBinding(typeof(TopBeersPresenter))]
    public partial class TopBeers : MvpUserControl<TopBeersViewModel>, IInitializableView<TopBeersViewModel>
    {
        public event EventHandler<EventArgs> Initialized;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Initialized?.Invoke(this, EventArgs.Empty);

            this.RankingGridView.DataSource = this.Model.Rankings;
            this.RankingGridView.DataBind();
        }
    }
}
