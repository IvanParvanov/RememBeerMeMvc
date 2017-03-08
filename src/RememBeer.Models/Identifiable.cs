using RememBeer.Models.Contracts;

namespace RememBeer.Models
{
    public abstract class Identifiable : IIdentifiable
    {
        public int Id { get; set; }
    }
}
