using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using RememBeer.Business.Logic.Admin.Brewery;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Models.Contracts;
using RememBeer.WebClient.BaseClasses;

using WebFormsMvp;

namespace RememBeer.WebClient.Admin
{
    [PresenterBinding(typeof(BreweryPresenter))]
    public partial class Brewery : BaseMvpPage<SingleBreweryViewModel>, ISingleBreweryView
    {
        public event EventHandler<ICreateBeerEventArgs> BreweryAddBeer;

        public event EventHandler<IIdentifiableEventArgs<int>> BreweryRemoveBeer;

        public event EventHandler<IBreweryUpdateEventArgs> BreweryUpdate;

        public event EventHandler<IIdentifiableEventArgs<string>> Initialized;

        public string SuccessMessageText
        {
            get
            {
                return this.Notifier.SuccessText;
            }

            set
            {
                this.Notifier.SuccessText = value;
            }
        }

        public bool SuccessMessageVisible
        {
            get
            {
                return this.Notifier.SuccessVisible;
            }

            set
            {
                this.Notifier.SuccessVisible = value;
            }
        }

        public string ErrorMessageText
        {
            get
            {
                return this.Notifier.ErrorText;
            }

            set
            {
                this.Notifier.ErrorText = value;
            }
        }

        public bool ErrorMessageVisible
        {
            get
            {
                return this.Notifier.ErrorVisible;
            }

            set
            {
                this.Content.Visible = !value;
                this.Notifier.ErrorVisible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = this.Request.QueryString["id"];
            var args = this.EventArgsFactory.CreateIdentifiableEventArgs(id);
            this.Initialized?.Invoke(this, args);
            if (this.Model.Brewery != null && !this.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BreweryDetails_OnModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            this.BreweryDetails.ChangeMode(e.NewMode);
            this.BindData();
        }

        protected void BreweryDetails_OnItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            var id = int.Parse((string)e.NewValues["Id"]);
            var descr = (string)e.NewValues["Description"];
            var name = (string)e.NewValues["Name"];
            var country = (string)e.NewValues["Country"];

            var args = this.EventArgsFactory.CreateBreweryUpdateEventArgs(id, descr, name, country);
            this.BreweryUpdate?.Invoke(this, args);
            this.BreweryDetails.ChangeMode(DetailsViewMode.ReadOnly);
            this.BindData();
        }

        private void BindData()
        {
            this.BeersRepeater.DataSource = this.Model.Brewery.Beers;
            this.BreweryDetails.DataSource = new List<IBrewery>()
                                             {
                                                 this.Model.Brewery
                                             };
            this.BreweryDetails.DataBind();
            this.BeersRepeater.DataBind();
        }

        protected void CreateBeer_OnClick(object sender, EventArgs e)
        {
            var breweryId = int.Parse(this.Request.QueryString["id"]);
            var beerTypeId = int.Parse(this.HiddenBeerTypeId.Value);
            var beerName = this.BeerNameTb.Text;

            var args = this.EventArgsFactory.CreateCreateBeerEventArgs(breweryId, beerTypeId, beerName);
            this.BreweryAddBeer?.Invoke(this, args);
            this.BindData();
        }

        protected void OnBeerCommand(object sender, CommandEventArgs e)
        {
            var id = int.Parse((string)e.CommandArgument);
            var args = this.EventArgsFactory.CreateIdentifiableEventArgs(id);
            this.BreweryRemoveBeer?.Invoke(this, args);

            this.BindData();
        }
    }
}
