namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart : DeletableEntity, IAuditInfo, IKeyEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UniqueKey { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [InverseProperty("Cart")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
