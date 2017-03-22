using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace RememBeer.MvcClient.Areas.Admin
{
    [ExcludeFromCodeCoverage]
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
