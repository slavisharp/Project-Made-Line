namespace MadeLine.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderItem : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(10)]
        public string ProductSKU { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductDiscountValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductDiscountPercentage { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductFinalPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemPrice { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
