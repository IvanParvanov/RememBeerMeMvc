using System;

using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Business.Logic.Reviews.Common.ViewModels;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Reviews.Default.Contracts
{
    public interface IReviewDetailsView : IView<BeerReviewViewModel>
    {
        bool NotFoundVisible { get; set; }

        /// <summary>
        /// Triggered when the view is being initialized.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<int>> OnInitialise;
    }
}
