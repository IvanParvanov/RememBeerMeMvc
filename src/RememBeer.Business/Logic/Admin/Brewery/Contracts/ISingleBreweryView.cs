using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Admin.Brewery.Contracts
{
    public interface ISingleBreweryView : IView<SingleBreweryViewModel>, IViewWithErrors, IViewWithSuccess
    {
        /// <summary>
        /// Triggered when the view is being initialized.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<string>> Initialized;

        /// <summary>
        /// Triggered when a brewery needs to be updated.
        /// </summary>
        event EventHandler<IBreweryUpdateEventArgs> BreweryUpdate;

        /// <summary>
        /// Triggered when a new beer needs to be added to a brewery.
        /// </summary>
        event EventHandler<ICreateBeerEventArgs> BreweryAddBeer;

        /// <summary>
        /// Triggered when a new beer needs to be removed from a brewery.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<int>> BreweryRemoveBeer;
    }
}
