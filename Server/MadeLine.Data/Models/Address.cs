namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(1000)]
        public string AddressLine1 { get; set; }

        [MaxLength(1000)]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(60)]
        public string City { get; set; }

        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        [InverseProperty("BillingAddress")]
        public virtual ICollection<Order> BillingOrders { get; set; }

        [InverseProperty("ShippingAddress")]
        public virtual ICollection<Order> ShippigOrders { get; set; }
    }
}
