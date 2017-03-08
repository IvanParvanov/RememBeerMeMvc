using System;

using RememBeer.Business.Logic.Common.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;

using WebFormsMvp;

namespace RememBeer.Business.Logic.Admin.ManageUsers.Contracts
{
    public interface IManageUsersView : IView<ManageUsersViewModel>,
                                        IViewWithErrors,
                                        IViewWithSuccess,
                                        IPaginatedView
    {
        /// <summary>
        /// Triggered when a user needs to be disabled.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<string>> UserDisable;

        /// <summary>
        /// Triggered when a user needs to be enabled.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<string>> UserEnable;

        /// <summary>
        /// Triggered when a user needs to be added to the Administrator role.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<string>> UserMakeAdmin;

        /// <summary>
        /// Triggered when a user needs to be Removed from to the Administrator role.
        /// </summary>
        event EventHandler<IIdentifiableEventArgs<string>> UserRemoveAdmin;

        /// <summary>
        /// Triggered when a user's profile information needs to be updated.
        /// </summary>
        event EventHandler<IUserUpdateEventArgs> UserUpdate;

        /// <summary>
        /// Triggered when a user search is performed.
        /// </summary>
        event EventHandler<ISearchEventArgs> UserSearch;
    }
}
