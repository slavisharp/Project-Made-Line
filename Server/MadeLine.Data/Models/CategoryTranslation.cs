namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryTranslation : IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public TranslationLanguage Language { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
