namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Core.Managers;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateProductViewModel : ICreateProductModel
    {
        [Required]
        [MaxLength(50)]
        public string Alias { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public IEnumerable<int> CategoryIds { get; set; }

        public string Description { get; set; }

        public int? HighlightImageId { get; set; }
        
        [Required]
        public int MainImageId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IEnumerable<int> SizeIds { get; set; }

        [Required]
        [MaxLength(10)]
        public string SKUCode { get; set; }

        [Required]
        public ProductTargetType TargetType { get; set; }
        
        public IEnumerable<int> GalleryImageIds { get; set; }
    }
}
