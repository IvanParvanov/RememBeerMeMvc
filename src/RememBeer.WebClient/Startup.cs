using System.Web.Http;
using System.Web.Routing;

using Microsoft.Owin;

using Owin;

using RememBeer.WebClient.App_Start;

[assembly: OwinStartup(typeof(RememBeer.WebClient.Startup))]
namespace RememBeer.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = NinjectWebCommon.Kernel;

            this.ConfigureAuth(app, kernel);

            RouteTable.Routes.MapHttpRoute(
                                           name: "DefaultApi",
                                           routeTemplate: "api/{controller}/{id}",
                                           defaults: new
                                                     {
                                                         id = RouteParameter.Optional
                                                     }
                                          );
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
