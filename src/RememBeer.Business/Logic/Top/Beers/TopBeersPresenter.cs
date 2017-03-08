using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Top.Common;
using RememBeer.Common.Constants;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Top.Beers
{
    public class TopBeersPresenter : TopPresenterBase<IInitializableView<TopBeersViewModel>>
    {
        public TopBeersPresenter(ITopBeersService topBeersService, IInitializableView<TopBeersViewModel> view)
            : base(topBeersService, view)
        {
            this.View.Initialized += this.OnViewInitialize;
        }

        private void OnViewInitialize(object sender, EventArgs e)
        {
            var beers = this.TopBeersService.GetTopBeers(Constants.TopBeersCount);
            this.View.Model.Rankings = beers;
        }
    }
}
