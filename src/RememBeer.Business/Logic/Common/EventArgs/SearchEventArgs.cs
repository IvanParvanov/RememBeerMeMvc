using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Common.EventArgs
{
    public class SearchEventArgs : ISearchEventArgs
    {
        public SearchEventArgs(string pattern)
        {
            this.Pattern = pattern;
        }

        public string Pattern { get; }
    }
}
