namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CountryTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public TranslationLanguage Language { get; set; }

        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
