using Ninject;

using RememBeer.Business.Logic.Common;
using RememBeer.Models.Identity.Contracts;

using WebFormsMvp.Web;

namespace RememBeer.WebClient.BaseClasses
{
    public class BaseMvpPage<TModel> : MvpPage<TModel> where TModel : class, new()
    {
        [Inject]
        public IIdentityHelper IdentityHelper { protected get; set; }

        [Inject]
        public ICustomEventArgsFactory EventArgsFactory { protected get; set; }
    }
}
