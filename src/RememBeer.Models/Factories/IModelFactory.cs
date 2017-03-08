using RememBeer.Models.Contracts;

namespace RememBeer.Models.Factories
{
    public interface IModelFactory : IRankFactory
    {
        IApplicationUser CreateApplicationUser(string username, string email);
    }
}
