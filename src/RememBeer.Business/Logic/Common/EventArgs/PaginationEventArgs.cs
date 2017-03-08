using RememBeer.Business.Logic.Common.EventArgs.Contracts;

namespace RememBeer.Business.Logic.Common.EventArgs
{
    public class PaginationEventArgs : IPaginationEventArgs
    {
        public PaginationEventArgs(int startRowIndex, int pageSize)
        {
            this.StartRowIndex = startRowIndex;
            this.PageSize = pageSize;
        }

        public int StartRowIndex { get; }

        public int PageSize { get; }
    }
}
