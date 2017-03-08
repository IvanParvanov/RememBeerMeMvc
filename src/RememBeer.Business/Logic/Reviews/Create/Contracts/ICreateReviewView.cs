using System;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Reviews.Create.Contracts
{
    public interface ICreateReviewView : IView<StatelessViewModel>, IViewWithErrors
    {
        /// <summary>
        /// Triggered when a new review needs to be created.
        /// </summary>
        event EventHandler<IBeerReviewInfoEventArgs> OnCreateReview;
    }
}
