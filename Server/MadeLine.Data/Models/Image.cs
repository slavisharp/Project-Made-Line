namespace MadeLine.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image : IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(127)]
        public string Name { get; set; }

        [Required]
        [MinLength(5), MaxLength(127)]
        public string GuidName { get; set; }

        [MaxLength(127)]
        public string Extension { get; set; }

        [MaxLength(127)]
        public string MimeType { get; set; }

        [Required]
        [MaxLength(200)]
        public string URL { get; set; }

        [InverseProperty("MainImage")]
        public virtual ICollection<Brand> BrandMainImages { get; set; }

        [InverseProperty("MainImage")]
        public virtual ICollection<Product> ProductMainImages { get; set; }

        [InverseProperty("HighlightImage")]
        public virtual ICollection<Product> ProductHighlightedImages { get; set; }

        [InverseProperty("Image")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }


    }
}
