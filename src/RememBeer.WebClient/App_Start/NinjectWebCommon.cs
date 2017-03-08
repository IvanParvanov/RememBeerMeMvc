using System.Web.Http;
using System;
using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;

using RememBeer.CompositionRoot;
using RememBeer.CompositionRoot.Compositions;
using RememBeer.CompositionRoot.Resolvers;

using WebFormsMvp.Binder;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RememBeer.WebClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RememBeer.WebClient.App_Start.NinjectWebCommon), "Stop")]

namespace RememBeer.WebClient.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        private static IKernel kernel;

        public static IKernel Kernel
        {
            get
            {
                return kernel;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Kernel));
                }

                kernel = value;
            }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            Kernel = new StandardKernel(
                new NinjectSettings() { LoadExtensions = true }
                );
            try
            {
                Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(Kernel);

                return Kernel;
            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var composition = new DefaultComposition();
            composition.RegisterServices(kernel);

            PresenterBinder.Factory = kernel.Get<IPresenterFactory>();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
        }        
    }
}
