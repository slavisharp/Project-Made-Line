namespace MadeLine.Api.ViewModels
{
    using System.Collections.Generic;

    public class SimpleBadRequestViewModel
    {
        public SimpleBadRequestViewModel(string error)
        {
            this.Error = error;
        }

        public string Error { get; set; }
    }

    public class BadRequestViewModel
    {
        public BadRequestViewModel(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }

    public class BadRequestViewModel<T>
    {
        public BadRequestViewModel(IEnumerable<T> errors)
        {
            this.Errors = errors;
        }

        public IEnumerable<T> Errors { get; set; }
    }
}
