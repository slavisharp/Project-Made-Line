namespace MadeLine.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductColor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Value { get; set; }

        [InverseProperty("Color")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
