using Ninject;

namespace RememBeer.CompositionRoot.Compositions.Contracts
{
    public interface IModuleComposition
    {
        void RegisterServices(IKernel kernel);
    }
}
