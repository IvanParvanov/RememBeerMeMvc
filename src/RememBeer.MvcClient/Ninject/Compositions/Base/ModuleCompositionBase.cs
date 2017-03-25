using System.Diagnostics.CodeAnalysis;

using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Syntax;

using RememBeer.MvcClient.Ninject.Compositions.Contracts;

namespace RememBeer.MvcClient.Ninject.Compositions.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class ModuleCompositionBase : IModuleComposition
    {
        public void RegisterServices(IKernel kernel)
        {
            BindDefaultInterfaces(kernel);
            this.LoadModules(kernel);
        }

        protected abstract void LoadModules(IKernel kernel);

        private static void BindDefaultInterfaces(IBindingRoot kernel)
        {
            kernel.Bind(x => x.FromAssembliesMatching("RememBeer.*")
                              .SelectAllClasses()
                              .BindDefaultInterface());
        }
    }
}
