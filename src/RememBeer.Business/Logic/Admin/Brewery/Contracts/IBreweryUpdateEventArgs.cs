using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Admin.Brewery.Contracts
{
    public interface IBreweryUpdateEventArgs : IIdentifiableEventArgs<int>
    {
        string Description { get; set; }

        string Name { get; set; }

        string Country { get; set; }
    }
}
