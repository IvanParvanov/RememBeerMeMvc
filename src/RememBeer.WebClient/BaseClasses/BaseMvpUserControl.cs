using Ninject;

using RememBeer.Business.Logic.Common;

using WebFormsMvp.Web;

namespace RememBeer.WebClient.BaseClasses
{
    public class BaseMvpUserControl<TModel> : MvpUserControl<TModel> where TModel : class, new()
    {
        [Inject]
        public ICustomEventArgsFactory EventArgsFactory { protected get; set; }
    }
}