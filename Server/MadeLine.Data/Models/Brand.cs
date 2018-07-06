namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Brand : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Alias { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ImageId { get; set; }
        public virtual Image MainImage { get; set; }
        
        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        [InverseProperty("Brand")]
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<BrandTranslation> Translations { get; set; }
    }
}
