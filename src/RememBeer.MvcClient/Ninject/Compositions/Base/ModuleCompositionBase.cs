﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

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
            kernel.Bind(x =>
                        {
                            var assemblies = AppDomain.CurrentDomain
                                                      .GetAssemblies()
                                                      .Where(a => a.FullName.StartsWith("RememBeer."));

                            foreach (var assembly in assemblies)
                            {
                                var assemblyLocation = assembly.Location;
                                var directoryPath = Path.GetDirectoryName(assemblyLocation);
                                x.FromAssembliesInPath(directoryPath)
                                 .SelectAllClasses()
                                 .BindDefaultInterface();
                            }
                        });
        }
    }
}
