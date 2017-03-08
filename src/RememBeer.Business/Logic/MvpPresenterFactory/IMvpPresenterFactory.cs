using System;

using WebFormsMvp;

namespace RememBeer.Business.Logic.MvpPresenterFactory
{
    public interface IMvpPresenterFactory
    {
        IPresenter GetPresenter(Type presenterType, IView viewInstance);
    }
}
