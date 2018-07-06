namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CampaignTranslation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public TranslationLanguage Language { get; set; }

        [Required]
        public int CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}
