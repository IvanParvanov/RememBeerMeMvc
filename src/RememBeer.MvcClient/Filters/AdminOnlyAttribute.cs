using System.Web.Mvc;

using RememBeer.Common.Constants;

namespace RememBeer.MvcClient.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if (!httpContext.Request.IsAuthenticated || !httpContext.User.IsInRole(Constants.AdminRole))
            {
                filterContext.Result = new ViewResult
                                       {
                                           ViewName = "Error",
                                           ViewData = filterContext.Controller.ViewData
                                       };

                filterContext.Controller.ViewData["ErrorMessage"] = "You must be an administrator to access this area.";
            }
        }
    }
}
