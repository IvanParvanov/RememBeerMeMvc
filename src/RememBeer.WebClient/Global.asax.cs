using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace RememBeer.WebClient
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //RouteTable.Routes.MapHttpRoute(
            //                               name: "DefaultApi",
            //                               routeTemplate: "api/{controller}/{id}",
            //                               defaults: new { id = System.Web.Http.RouteParameter.Optional }
            //    );
        }

        //public void Application_Error(object sender, EventArgs e)
        //{
        //    // Code that runs when an unhandled error occurs

        //    // Get the exception object.
        //    Exception exc = this.Server.GetLastError();

        //    // Handle HTTP errors
        //    if (exc.GetType() == typeof(HttpException))
        //    {
        //        int errorCode = ((HttpException)exc).GetHttpCode();

        //        if (404 == errorCode)
        //        {
        //            this.Server.ClearError();
        //            this.Server.Transfer("~/404.aspx");
        //            return;
        //        }

        //        //Redirect HTTP errors to HttpError page
        //        this.Server.Transfer("HttpErrorPage.aspx");
        //    }

        //    // For other kinds of errors give the user some information
        //    // but stay on the default page
        //    this.Response.Write("<h2>Global Page Error</h2>\n");
        //    this.Response.Write(
        //                        "<p>" + exc.Message + "</p>\n");
        //    this.Response.Write("Return to the <a href='Default.aspx'>" +
        //                        "Default Page</a>\n");

        //    // Log the exception and notify system operators
        //    //ExceptionUtility.LogException(exc, "DefaultPage");
        //    //ExceptionUtility.NotifySystemOps(exc);

        //    // Clear the error from the server
        //    this.Server.ClearError();
        //}
    }
}
