using Ninject;

using RememBeer.CompositionRoot.Compositions.Base;
using RememBeer.CompositionRoot.NinjectModules;

namespace RememBeer.CompositionRoot.Compositions
{
    public class DefaultComposition : ModuleCompositionBase
    {
        protected override void LoadModules(IKernel kernel)
        {
            kernel.Load(new AuthNinjectModule());
            kernel.Load(new BusinessNinjectModule());
            kernel.Load(new DataNinjectModule());
        }
    }
}
