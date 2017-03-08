using System;

using RememBeer.Services.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Admin.Common
{
    public class BreweryServicePresenter<TView> : Presenter<TView> where TView : class, IView
    {
        private readonly IBreweryService breweryService;

        public BreweryServicePresenter(IBreweryService breweryService, TView view)
            : base(view)
        {
            if (breweryService == null)
            {
                throw new ArgumentNullException(nameof(breweryService));
            }

            this.breweryService = breweryService;
        }

        protected IBreweryService BreweryService => this.breweryService;
    }
}
