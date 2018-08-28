namespace MadeLine.Api.ViewModels
{
    public class OkObjectViewModel
    {
        public OkObjectViewModel(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }

    public class OkObjectViewModel<T> : OkObjectViewModel
        where T: class
    {
        public OkObjectViewModel(string message, T data) : base(message)
        {
            this.Data = data;
        }

        public T Data { get; set; }
    }
}
