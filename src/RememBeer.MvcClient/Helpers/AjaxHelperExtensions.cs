using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace RememBeer.MvcClient.Helpers
{
    public static class AjaxHelperExtensions
    {
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
                                       OnFailure = "handleAjaxError"
                                   },
                                   new { @class = className });
        }
    }
}