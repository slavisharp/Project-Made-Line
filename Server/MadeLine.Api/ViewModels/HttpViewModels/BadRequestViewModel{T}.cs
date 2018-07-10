namespace MadeLine.Api.ViewModels
{
    using System.Collections.Generic;

    public class SimpleBadRequestViewModel
    {
        public string Error { get; set; }
    }

    public class BadRequestViewModel
    {
        public IEnumerable<string> Errors { get; set; }
    }

    public class BadRequestViewModel<T>
    {
        public IEnumerable<T> Errors { get; set; }
    }
}
