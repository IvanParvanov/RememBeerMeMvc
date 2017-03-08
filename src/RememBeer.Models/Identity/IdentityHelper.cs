using System;
using System.Web;

using RememBeer.Models.Identity.Contracts;

namespace RememBeer.Models.Identity
{
    public class IdentityHelper : IIdentityHelper
    {
        // Used for XSRF when linking external logins
        public string XsrfKey => "XsrfId";

        public string ProviderNameKey => "providerName";

        public string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[this.ProviderNameKey];
        }

        public const string CodeKey = "code";

        public string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";

        public string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "="
                              +
                              HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) &&
                   ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) ||
                    (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public void RedirectToReturnUrl(string returnUrl, HttpResponseBase response)
        {
            if (!string.IsNullOrEmpty(returnUrl) && this.IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }

        public void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!string.IsNullOrEmpty(returnUrl) && this.IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }

        public string GetReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && this.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return "~/";
        }
    }
}
