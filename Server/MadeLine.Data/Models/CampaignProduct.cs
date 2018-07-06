namespace MadeLine.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CampaignProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public enum DiscountType
    {
        Value = 1,
        Percentage = 2
    }
}
