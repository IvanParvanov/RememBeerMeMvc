using System;

using RememBeer.Services.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Top.Common
{
    public class TopPresenterBase<TView> : Presenter<TView> where TView : class, IView
    {
        private readonly ITopBeersService topService;

        public TopPresenterBase(ITopBeersService topService, TView view)
            : base(view)
        {
            if (topService == null)
            {
                throw new ArgumentNullException(nameof(topService));
            }

            this.topService = topService;
        }

        protected ITopBeersService TopBeersService => this.topService;
    }
}
