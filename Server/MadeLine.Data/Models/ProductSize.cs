namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductSize : IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product  Product { get; set; }

        [Required]
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        public bool Available { get; set; }
    }
}
