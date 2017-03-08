using System.Collections.Generic;

namespace RememBeer.Data.Repositories
{
    public interface IDataModifiedResult
    {
        bool Successful { get; }

        IEnumerable<string> Errors { get; }
    }
}
