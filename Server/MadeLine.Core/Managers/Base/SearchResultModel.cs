namespace MadeLine.Core.Managers
{
    using System.Linq;

    public class SearchResultModel<T> : ISearchResultModel<T>
        where T : class
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public int TotalCount { get; set; }

        public IQueryable<T> List { get; set; }
    }
}
