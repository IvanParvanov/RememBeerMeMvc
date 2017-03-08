using System;

using WebFormsMvp;
using WebFormsMvp.Binder;

namespace RememBeer.Business.Logic.MvpPresenterFactory
{
    public class MvpPresenterFactory : IPresenterFactory
    {
        private readonly IMvpPresenterFactory presenterFactory;

        public MvpPresenterFactory(IMvpPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory;
        }

        /// <summary>
        /// Creates a new instance of the specific presenter type, for the specified
        /// view type and instance.
        /// </summary>
        /// <param name="presenterType">The type of presenter to create.</param>
        /// <param name="viewType">The type of the view as defined by the binding that matched.</param>
        /// <param name="viewInstance">The view instance to bind this presenter to.</param>
        /// <returns>An instantitated presenter.</returns>
        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            return this.presenterFactory.GetPresenter(presenterType, viewInstance);
        }

        /// <summary>
        /// Releases the specified presenter from any of its lifestyle demands.
        /// This method's activities are implementation specific - for example,
        /// an IoC based factory would return the presenter to the container.
        /// </summary>
        /// <param name="presenter">The presenter to release.</param>
        public void Release(IPresenter presenter)
        {
            var disposable = presenter as IDisposable;
            disposable?.Dispose();
        }
    }
}
