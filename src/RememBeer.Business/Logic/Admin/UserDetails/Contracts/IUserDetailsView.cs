using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Admin.UserDetails.Contracts
{
    public interface IUserDetailsView : IView<UserDetailsViewModel>, IViewWithErrors
    {
        event EventHandler<IIdentifiableEventArgs<string>> Initialized;
    }
}
