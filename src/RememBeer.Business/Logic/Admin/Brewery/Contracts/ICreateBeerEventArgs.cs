using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Admin.Brewery.Contracts
{
    public interface ICreateBeerEventArgs : IIdentifiableEventArgs<int>
    {
        int BeerTypeId { get; }

        string BeerName { get; }
    }
}
