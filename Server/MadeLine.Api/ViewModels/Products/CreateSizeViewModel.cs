namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Core.Managers;
    using System.ComponentModel.DataAnnotations;

    public class CreateSizeViewModel : ICreateSizeModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
    
    public class UpdateSizeViewModel : CreateSizeViewModel, IUpdateSizeModel
    {
        [Required]
        public int Id { get; set; }
    }
}
