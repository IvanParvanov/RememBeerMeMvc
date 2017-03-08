using System;

using RememBeer.Business.Logic.Account.Common.ViewModels;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Account.ForgotPassword.Contracts
{
    public interface IForgotPasswordView : IView<StatelessViewModel>
    {
        /// <summary>
        /// Triggered when a user sends a reset password request.
        /// </summary>
        event EventHandler<IForgotPasswordEventArgs> OnForgot;

        string FailureMessage { get; set; }

        bool ErrorMessageVisible { get; set; }

        bool LoginFormVisible { get; set; }

        bool DisplayEmailVisible { get; set; }
    }
}
