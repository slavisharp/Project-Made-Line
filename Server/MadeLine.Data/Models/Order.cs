namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order : DeletableEntity, IAuditInfo, IKeyEntity<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Number { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalItemsPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrderPrice { get; set; }

        [Required]
        public int BillingAddressId { get; set; }
        public virtual Address BillingAddress { get; set; }

        [Required]
        public int ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [Required]
        public OrderStatusType CurrentStatus { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderStatus> StatusHistory { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
