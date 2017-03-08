using System;

using RememBeer.Business.Logic.Account.Common.ViewModels;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Account.Register.Contracts
{
    public interface IRegisterView : IView<StatelessViewModel>
    {
        /// <summary>
        /// Triggered when a new user needs to be registered.
        /// </summary>
        event EventHandler<IRegisterEventArgs> OnRegister;

        string ErrorMessageText { get; set; }
    }
}
