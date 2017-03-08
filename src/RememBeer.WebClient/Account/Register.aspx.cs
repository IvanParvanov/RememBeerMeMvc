using System;

using RememBeer.Business.Logic.Account.Common.ViewModels;
using RememBeer.Business.Logic.Account.Register;
using RememBeer.Business.Logic.Account.Register.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Account
{
    [PresenterBinding(typeof(RegisterPresenter))]
    public partial class Register : BaseMvpPage<StatelessViewModel>, IRegisterView
    {
        public event EventHandler<IRegisterEventArgs> OnRegister;

        public string ErrorMessageText
        {
            get
            {
                return this.ErrorMessage.Text;
            }
            set
            {
                this.ErrorMessage.Text = value;
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                var args = this.EventArgsFactory.CreateRegisterEventArg(this.Email.Text, this.Email.Text, this.Password.Text);
                this.OnRegister?.Invoke(this, args);
            }
        }
    }
}
