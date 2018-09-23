namespace MadeLine.Api.ViewModels.Vlogs
{
    using MadeLine.Core.Managers;
    using System.ComponentModel.DataAnnotations;

    public class CreateVlogViewModel : ICreateVlogModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Alias { get; set; }

        [Required]
        public string EmbededVideo { get; set; }

        [Required]
        public int ThumbImageId { get; set; }
    }

    public class UpdateVlogViewModel : CreateVlogViewModel, IUpdateVlogModel
    {
        [Required]
        public int Id { get; set; }
    }
}
