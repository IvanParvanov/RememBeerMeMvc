using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Admin.ManageUsers.Contracts
{
    public interface IUserUpdateEventArgs : IIdentifiableEventArgs<string>
    {
        string Email { get; }

        string UserName { get; }

        bool IsConfirmed { get; }
    }
}
