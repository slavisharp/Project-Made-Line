namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Core.Managers;
    using System.ComponentModel.DataAnnotations;

    public class CreateProductColorViewModel : ICreateColorModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Value { get; set; }
    }
    
    public class UpdateProductColorViewModel : CreateProductColorViewModel, IUpdateColorModel
    {
        [Required]
        public int Id { get; set; }
    }
}
