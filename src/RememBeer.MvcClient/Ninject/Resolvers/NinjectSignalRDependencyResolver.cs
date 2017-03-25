using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Bytes2you.Validation;

using Microsoft.AspNet.SignalR;

using Ninject;

namespace RememBeer.MvcClient.Ninject.Resolvers
{
    [ExcludeFromCodeCoverage]
    public class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            Guard.WhenArgument(kernel, nameof(kernel)).IsNull().Throw();

            this.kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}
