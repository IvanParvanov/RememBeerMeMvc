using System;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Common.Contracts
{
    public interface IInitializableView<T> : IView<T> where T : class, new()
    {
        /// <summary>
        /// Triggered when the view is being initialized.
        /// </summary>
        event EventHandler<System.EventArgs> Initialized;
    }
}
