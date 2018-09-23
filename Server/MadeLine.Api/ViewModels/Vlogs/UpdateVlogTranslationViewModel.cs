namespace MadeLine.Api.ViewModels.Vlogs
{
    using MadeLine.Core.Managers;
    using MadeLine.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class UpdateVlogTranslationViewModel : IUpdateTranslationVlogModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public TranslationLanguage Language { get; set; }
}
}
