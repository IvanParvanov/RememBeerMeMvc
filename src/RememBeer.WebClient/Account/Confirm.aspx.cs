using System;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Account.Confirm;
using RememBeer.Business.Logic.Account.Confirm.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Account
{
    [PresenterBinding(typeof(ConfirmPresenter))]
    public partial class Confirm : BaseMvpPage<StatelessViewModel>, IConfirmView
    {
        public event EventHandler<IConfirmEventArgs> OnSubmit;

        public bool SuccessPanelVisible { get; set; }

        public bool ErrorPanelVisible { get; set; }

        public string StatusMessage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var code = this.IdentityHelper.GetCodeFromRequest(this.Request);
            var userId = this.IdentityHelper.GetUserIdFromRequest(this.Request);
            var args = this.EventArgsFactory.CreateConfirmEventArgs(userId, code);

            this.OnSubmit?.Invoke(this, args);
        }
    }
}
