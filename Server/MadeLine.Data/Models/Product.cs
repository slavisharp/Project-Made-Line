namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Alias { get; set; }

        [Required]
        [MaxLength(10)]
        public string SKUCode { get; set; }

        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public bool IsHighlighted { get; set; }

        [Required]
        public int MainImageId { get; set; }
        public virtual Image MainImage { get; set; }

        [Required]
        public int HighlightImageId { get; set; }
        public virtual Image HighlightImage { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<ProductImage> Images { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required]
        public int ColorId { get; set; }
        public virtual ProductColor Color { get; set; }

        [Required]
        public ProductStatus Status { get; set; }
        
        [Required]
        public ProductTargetType TargetType { get; set; }
                
        [InverseProperty("Product")]
        public virtual ICollection<ProductSize> Sizes { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<ProductCategory> Categories { get; set; }
        
        public int? ParentProductId { get; set; }
        public virtual Product ParentProduct { get; set; }

        [InverseProperty("ParentProduct")]
        public virtual ICollection<Product> ProductVariations { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<CampaignProduct> Campaings { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public virtual ICollection<ProductTranslation> Translations { get; set; }
    }

    public enum ProductStatus
    {
        Draft = 1,
        Published = 2,
        Archived = 3
    }

    public enum ProductTargetType
    {
        Women = 1,
        Men = 2
    }
}
