using System;
using System.Collections.Generic;

using RememBeer.Business.Logic.Account.Common.ViewModels;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Account.ManagePassword.Contracts
{
    public interface IManagePasswordView : IView<StatelessViewModel>
    {
        /// <summary>
        /// Triggered when a user's password needs to be changed.
        /// </summary>
        event EventHandler<IChangePasswordEventArgs> ChangePassword;

        string SuccessMessage { get; set; }

        void AddErrors(IEnumerable<string> errors);
    }
}
