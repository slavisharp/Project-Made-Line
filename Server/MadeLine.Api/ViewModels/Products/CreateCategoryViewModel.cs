namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Core.Managers;
    using MadeLine.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryViewModel : ICreateCategoryModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
    
    public class UpdateCategoryViewModel : CreateCategoryViewModel, IUpdateCategoryModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class UpdateCategoryTranslationViewModel : UpdateCategoryViewModel, ICategoryTranslationModel
    {
        public TranslationLanguage Language { get; set; }
    }
}
