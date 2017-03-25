using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using Microsoft.AspNet.SignalR.Hubs;

using Moq;

using Ninject;

using NUnit.Framework;

using RememBeer.Models.Identity.Contracts;
using RememBeer.MvcClient;
using RememBeer.MvcClient.App_Start;
using RememBeer.MvcClient.Controllers;

namespace RememBeer.IntegrationTests.MvcClient.Ninject
{
    [TestFixture]
    public class CompositionRootTests
    {
        [Test]
        public void CompositionRoot_ShouldBeAbleToBuildAllRootTypes()
        {
            // Arrange
            NinjectWebCommon.Start();
            AutoMapConfig.RegisterMappings();

            var writer = new HtmlTextWriter(TextWriter.Null);
            var req = new HttpRequest("/asd.png", "http://localhost/asd.png", "");
            var res = new HttpResponse(writer);
            var httpContext = new HttpContext(req, res);
            HttpContext.Current = httpContext;

            var owinEnv = new Dictionary<string, object>
                          {
                              { "AspNet.Identity.Owin:" + typeof(IApplicationUserManager).AssemblyQualifiedName, new Mock<IApplicationUserManager>().Object },
                              { "AspNet.Identity.Owin:" + typeof(IApplicationSignInManager).AssemblyQualifiedName, new Mock<IApplicationSignInManager>().Object }
                          };
            httpContext.Items["owin.Environment"] = owinEnv;

            var mvcAssembly = typeof(HomeController).Assembly;
            var rootTypes = mvcAssembly.GetExportedTypes()
                                       .Where(type => typeof(IController).IsAssignableFrom(type) || typeof(IHub).IsAssignableFrom(type))
                                       .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition)
                                       .Where(type => type.Name.EndsWith("Controller") || type.Name.EndsWith("Hub"));

            // Act & Assert
            foreach (var type in rootTypes)
            {
                Assert.DoesNotThrow(() => NinjectWebCommon.Kernel.Get(type));
            }
        }
    }
}
