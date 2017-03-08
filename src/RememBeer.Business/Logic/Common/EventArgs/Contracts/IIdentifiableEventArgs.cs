namespace RememBeer.Business.Logic.Common.EventArgs.Contracts
{
    public interface IIdentifiableEventArgs<T>
    {
        T Id { get; set; }
    }
}
