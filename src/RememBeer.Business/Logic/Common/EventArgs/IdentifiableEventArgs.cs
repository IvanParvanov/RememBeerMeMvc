using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Common.EventArgs
{
    public class IdentifiableEventArgs<T> : IIdentifiableEventArgs<T>
    {
        public IdentifiableEventArgs(T id)
        {
            this.Id = id;
        }

        public T Id { get; set; }
    }
}
