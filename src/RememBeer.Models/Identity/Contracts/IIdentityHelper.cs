using System.Web;

namespace RememBeer.Models.Identity.Contracts
{
    public interface IIdentityHelper
    {
        string GetProviderNameFromRequest(HttpRequest request);

        string GetCodeFromRequest(HttpRequest request);

        string GetUserIdFromRequest(HttpRequest request);

        string GetResetPasswordRedirectUrl(string code, HttpRequest request);

        string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request);

        void RedirectToReturnUrl(string returnUrl, HttpResponse response);

        void RedirectToReturnUrl(string returnUrl, HttpResponseBase response);

        string GetReturnUrl(string returnUrl);

        string XsrfKey { get; }

        string ProviderNameKey { get; }
    }
}
