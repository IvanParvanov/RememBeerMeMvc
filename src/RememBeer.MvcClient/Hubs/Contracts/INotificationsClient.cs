namespace RememBeer.MvcClient.Hubs.Contracts
{
    public interface INotificationsClient
    {
        void OnFollowerReviewCreated(int id, string username);

        void ShowNotification(string message, string username, string lat, string lon);
    }
}