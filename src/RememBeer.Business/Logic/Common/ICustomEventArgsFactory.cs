using RememBeer.Business.Logic.Account.Confirm.Contracts;
using RememBeer.Business.Logic.Account.ForgotPassword.Contracts;
using RememBeer.Business.Logic.Account.Login.Contracts;
using RememBeer.Business.Logic.Account.ManagePassword.Contracts;
using RememBeer.Business.Logic.Account.Register.Contracts;
using RememBeer.Business.Logic.Admin.Brewery.Contracts;
using RememBeer.Business.Logic.Admin.ManageUsers.Contracts;
using RememBeer.Business.Logic.Common.EventArgs.Contracts;
using RememBeer.Business.Logic.Reviews.My.Contracts;
using RememBeer.Models.Contracts;

namespace RememBeer.Business.Logic.Common
{
    public interface ICustomEventArgsFactory
    {
        IConfirmEventArgs CreateConfirmEventArgs(string userId, string code);

        IForgotPasswordEventArgs CreateForgottenPasswordEventArgs(string email);

        ILoginEventArgs CreateLoginEventArgs(string email, string password, bool rememberMe);

        IChangePasswordEventArgs CreateChangePasswordEventArgs(string currentPassword,
                                                               string newPassword,
                                                               string userId);

        IRegisterEventArgs CreateRegisterEventArg(string userName, string email, string password);

        IBeerReviewInfoEventArgs CreateBeerReviewInfoEventArgs(IBeerReview beerReview);

        IBeerReviewInfoEventArgs CreateBeerReviewInfoEventArgs(IBeerReview beerReview, byte[] image);

        IIdentifiableEventArgs<T> CreateIdentifiableEventArgs<T>(T id);

        ISearchEventArgs CreateSearchEventArgs(string pattern);

        IBreweryUpdateEventArgs CreateBreweryUpdateEventArgs(int id, string description, string name, string country);

        IPaginationEventArgs CreatePaginationEventArgs(int startRowIndex, int pageSize);

        IUserReviewsEventArgs CreateUserReviewsEventArgs(int startRowIndex, int pageSize, string userId);

        IUserUpdateEventArgs CreateUserUpdateEventArgs(string id, string email, string userName, bool isConfirmed);

        ICreateBeerEventArgs CreateCreateBeerEventArgs(int id, int beerTypeId, string beerName);
    }
}
