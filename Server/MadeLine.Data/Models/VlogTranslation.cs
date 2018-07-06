namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VlogTranslation : IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public TranslationLanguage Language { get; set; }

        [Required]
        public int VlogId { get; set; }
        public virtual Vlog Vlog { get; set; }
    }
}
