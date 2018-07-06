namespace MadeLine.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<CountryTranslation> Translations { get; set; }
    }
}
