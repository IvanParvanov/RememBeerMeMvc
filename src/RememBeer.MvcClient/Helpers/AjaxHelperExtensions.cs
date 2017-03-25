using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace RememBeer.MvcClient.Helpers
{
    public static class AjaxHelperExtensions
    {
        /// <summary>
        /// Gets an AJAX anchor tag, which replaces the main body content.
        /// </summary>
        /// <param name="ajax">AjaxHelper</param>
        /// <param name="linkText">The text to be displayed</param>
        /// <param name="className">The class name of the tag</param>
        /// <param name="action">The name of the action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object representing route values</param>
        /// <returns></returns>
        public static MvcHtmlString PageRefreshActionLink(this AjaxHelper ajax, string linkText, string className, string action, string controller, object routeValues = null)
        {
            return ajax.ActionLink(linkText, action, controller,
                                   routeValues,
                                   new AjaxOptions()
                                   {
                                       HttpMethod = "GET",
                                       InsertionMode = InsertionMode.Replace,
                                       LoadingElementId = "loading",
                                       UpdateTargetId = "content",
                                       OnSuccess = "initMaterialize()",
                                       OnFailure = "notifier.handleAjaxError"
                                   },
                                   new { @class = className });
        }
    }
}
