namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Campaign : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int ThumbnailId { get; set; }
        public Image Thumbnail { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public virtual ICollection<CampaignProduct> Products { get; set; }

        public virtual ICollection<CampaignTranslation> Translations { get; set; }
    }
}
