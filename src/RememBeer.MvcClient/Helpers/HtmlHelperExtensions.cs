using System.Linq;
using System.Web.Mvc;

namespace RememBeer.MvcClient.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string actions = "", string controllers = "", string cssClass = "active")
        {
            var viewContext = html.ViewContext;
            var isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
            {
                viewContext = html.ViewContext.ParentActionViewContext;
            }

            var routeValues = viewContext.RouteData.Values;
            var currentAction = routeValues["action"].ToString().ToLower();
            var currentController = routeValues["controller"].ToString().ToLower();

            if (string.IsNullOrEmpty(actions))
            {
                actions = currentAction;
            }

            if (string.IsNullOrEmpty(controllers))
            {
                controllers = currentController;
            }

            var acceptedActions = actions.ToLower().Trim().Split(',').Distinct().ToArray();
            var acceptedControllers = controllers.ToLower().Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController)
                ? cssClass
                : string.Empty;
        }
    }
}
