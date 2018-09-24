namespace MadeLine.Api.ViewModels.Vlogs
{
    using System.Collections.Generic;

    public class VlogSearchResultViewModel
    {
        IEnumerable<VlogDetailsViewModel> List { get; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}
