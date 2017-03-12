using System.Diagnostics.CodeAnalysis;

using Ninject;

using RememBeer.MvcClient.Ninject.Compositions.Base;
using RememBeer.MvcClient.Ninject.NinjectModules;

namespace RememBeer.MvcClient.Ninject.Compositions
{
    [ExcludeFromCodeCoverage]
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
