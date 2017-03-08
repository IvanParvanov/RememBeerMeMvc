using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Common;
using RememBeer.Common.Constants;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Top.Breweries
{
    public class TopBreweriesPresenter : TopPresenterBase<IInitializableView<TopBreweriesViewModel>>
    {
        public TopBreweriesPresenter(ITopBeersService topService, IInitializableView<TopBreweriesViewModel> view)
            : base(topService, view)
        {
            this.View.Initialized += this.OnViewInitialized;
        }

        private void OnViewInitialized(object sender, EventArgs e)
        {
            var rankings = this.TopBeersService.GetTopBreweries(Constants.TopBeersCount);
            this.View.Model.Rankings = rankings;
        }
    }
}
