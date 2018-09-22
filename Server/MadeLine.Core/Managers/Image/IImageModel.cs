namespace MadeLine.Core.Managers
{
    using Microsoft.AspNetCore.Http;

    public interface IUploadImageModel
    {
        IFormFile File { get; set; }

        string DestinationFolder { get; set; }
    }
}
