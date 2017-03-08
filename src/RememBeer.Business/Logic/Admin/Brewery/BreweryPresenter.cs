using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Business.Logic.Admin.Common;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Data.Repositories;
using RememBeer.Services.Contracts;

namespace RememBeer.Business.Logic.Admin.Brewery
{
    public class BreweryPresenter : BreweryServicePresenter<ISingleBreweryView>
    {
        private const string NotFoundMessage = "Brewery not found!";
        private const string UpdateSuccessMessage = "Brewery updated!";
        private const string BeerCreatedSuccessMessage = "Beer has been added to this brewery!";
        private const string BeerDeletedSuccessMessage = "Beer has been removed from this brewery!";

        public BreweryPresenter(IBreweryService breweryService, ISingleBreweryView view)
            : base(breweryService, view)
        {
            this.View.BreweryUpdate += this.OnUpdateBrewery;
            this.View.Initialized += this.OnViewInitialized;
            this.View.BreweryAddBeer += this.OnViewBreweryAddBeer;
            this.View.BreweryRemoveBeer += this.OnViewBreweryRemoveBeer;
        }

        private void OnViewBreweryRemoveBeer(object sender, IIdentifiableEventArgs<int> e)
        {
            var id = e.Id;
            var result = this.BreweryService.DeleteBeer(id);
            this.HandleResult(result, BeerDeletedSuccessMessage);
        }

        private void OnViewBreweryAddBeer(object sender, ICreateBeerEventArgs e)
        {
            var breweryId = e.Id;
            var beertypeId = e.BeerTypeId;
            var beerName = e.BeerName;

            var result = this.BreweryService.AddNewBeer(breweryId, beertypeId, beerName);
            this.HandleResult(result, BeerCreatedSuccessMessage);
        }

        private void OnUpdateBrewery(object sender, IBreweryUpdateEventArgs e)
        {
            var result = this.BreweryService.UpdateBrewery(e.Id, e.Name, e.Country, e.Description);
            this.HandleResult(result, UpdateSuccessMessage);
        }

        private void OnViewInitialized(object sender, IIdentifiableEventArgs<string> e)
        {
            var id = e.Id;
            int intId;
            var isValidId = int.TryParse(id, out intId);

            if (isValidId)
            {
                var brewery = this.BreweryService.GetById(intId);
                if (brewery == null)
                {
                    this.ShowError(NotFoundMessage);
                }
                else
                {
                    this.View.Model.Brewery = brewery;
                }
            }
            else
            {
                this.ShowError(NotFoundMessage);
            }
        }

        private void HandleResult(IDataModifiedResult result, string successMessage)
        {
            if (result.Successful)
            {
                this.View.SuccessMessageText = successMessage;
                this.View.SuccessMessageVisible = true;
            }
            else
            {
                this.ShowError(string.Join(", ", result.Errors));
            }
        }

        private void ShowError(string message)
        {
            this.View.ErrorMessageText = message;
            this.View.ErrorMessageVisible = true;
        }
    }
}
