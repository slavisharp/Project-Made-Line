namespace MadeLine.Core.Managers
{
    using System.Linq;

    public interface ISearchResultModel<T>
        where T: class
    {
        int Page { get; set; }

        int PageSize { get; set; }

        int PageCount { get; }

        int TotalCount { get; }

        IQueryable<T> List { get; set; }
    }
}
