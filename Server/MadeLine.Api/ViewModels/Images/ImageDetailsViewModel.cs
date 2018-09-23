namespace MadeLine.Api.ViewModels.Images
{
    using MadeLine.Data.Models;

    public class ImageDetailsViewModel
    {
        public ImageDetailsViewModel()
        {
        }

        public ImageDetailsViewModel(Image entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Url = entity.URL;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }
    }
}
