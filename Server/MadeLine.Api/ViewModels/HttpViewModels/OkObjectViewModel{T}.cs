namespace MadeLine.Api.ViewModels
{
    public class OkObjectViewModel
    {
        public string Message { get; set; }
    }

    public class OkObjectViewModel<T> : OkObjectViewModel
        where T: class
    {
        public T Data { get; set; }
    }
}
