using System.Collections.Generic;
using System.Linq;

namespace RememBeer.Data.Repositories
{
    public class DataModifiedResult : IDataModifiedResult
    {
        public static IDataModifiedResult Success => new DataModifiedResult(true);

        private readonly IEnumerable<string> errors;

        private readonly bool isSuccessful;

        public DataModifiedResult(bool isSuccessfull, IEnumerable<string> errors = null)
        {
            this.isSuccessful = isSuccessfull;
            this.errors = errors;
        }

        public bool Successful => this.isSuccessful && (this.errors == null || !this.errors.Any());

        public bool SuccessfulWithErrors => this.isSuccessful && this.errors != null && this.errors.Any();

        public IEnumerable<string> Errors => this.errors;
    }
}
