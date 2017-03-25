using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin;

using Owin;

using RememBeer.MvcClient;
using RememBeer.MvcClient.App_Start;
using RememBeer.MvcClient.Hubs.UserIdProvider;

[assembly: OwinStartup(typeof(Startup))]

namespace RememBeer.MvcClient
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app, NinjectWebCommon.Kernel);

            var idProvider = new UserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
            app.MapSignalR();
        }
    }
}
