using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Ninject;
using Ninject.Syntax;

namespace RememBeer.CompositionRoot.Resolvers
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return this.resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return this.resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            var disposable = this.resolver as IDisposable;
            disposable?.Dispose();

            this.resolver = null;
        }
    }
}