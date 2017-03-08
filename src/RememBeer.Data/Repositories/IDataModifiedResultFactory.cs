using System.Collections.Generic;

namespace RememBeer.Data.Repositories
{
    public interface IDataModifiedResultFactory
    {
        IDataModifiedResult CreateDatabaseUpdateResult(bool isSuccessfull, IEnumerable<string> errors = null);
    }
}
