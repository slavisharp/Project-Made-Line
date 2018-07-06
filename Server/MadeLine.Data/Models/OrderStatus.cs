namespace MadeLine.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public OrderStatusType Type { get; set; }

        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }

    public enum OrderStatusType
    {
        Processing = 1,
        Confirmed = 2,
        Shipped = 3,
        Completed = 4
    }
}
