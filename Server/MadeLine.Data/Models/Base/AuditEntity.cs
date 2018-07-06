namespace MadeLine.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class AuditEntity
    {
        [Required]
        public DateTime ActionDate { get; set; }

        [Required]
        public AuditActionType ActionType { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum AuditActionType
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
