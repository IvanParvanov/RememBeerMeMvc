namespace RememBeer.Models.Contracts
{
    public interface IBeerType : IIdentifiable
    {
        string Type { get; set; }
    }
}
