using Ninject;

namespace RememBeer.MvcClient.Ninject.Compositions.Contracts
{
    public interface IModuleComposition
    {
        void RegisterServices(IKernel kernel);
    }
}
