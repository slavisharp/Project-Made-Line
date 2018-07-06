namespace MadeLine.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IDeletableEntity, IKeyEntity<string>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public long? FacebookId { get; set; }

        [MaxLength(2100)]
        public string PictureUrl { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [MaxLength(50)]
        public string RegisterIP { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Address> Addresses { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Order> Orders { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Brand> Brands { get; set; }


    }
}
