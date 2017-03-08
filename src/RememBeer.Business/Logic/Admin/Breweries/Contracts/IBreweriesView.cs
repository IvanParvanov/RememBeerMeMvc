using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Admin.Breweries.Contracts
{
    public interface IBreweriesView : IInitializableView<BreweriesViewModel>
    {
        /// <summary>
        /// Triggered when a brewery search is performed.
        /// </summary>
        event EventHandler<ISearchEventArgs> BrewerySearch;
    }
}
