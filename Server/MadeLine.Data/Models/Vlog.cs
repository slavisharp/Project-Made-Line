namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vlog : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Alias { get; set; }

        public int? ThumbnailId { get; set; }
        public virtual Image Thumbnail { get; set; }

        [Required]
        public string EmbededVideo { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public virtual ICollection<VlogTranslation> Translations { get; set; }
    }
}
