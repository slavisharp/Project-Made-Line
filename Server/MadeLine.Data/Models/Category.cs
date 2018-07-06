namespace MadeLine.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category : IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<ProductCategory> Products { get; set; }

        public virtual ICollection<CategoryTranslation> Translations { get; set; }
    }
}
