using System.Web.Mvc;

namespace RememBeer.MvcClient.Filters
{
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData
                };

                filterContext.Controller.ViewData["ErrorMessage"] = "The operation can only be perfomed asynchronously. Please ensure JavaScript is enabled.";
            }
        }
    }
}
