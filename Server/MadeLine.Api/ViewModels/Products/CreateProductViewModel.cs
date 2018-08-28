namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Core.Managers;
    using MadeLine.Data.Models;
    using System.Collections.Generic;

    public class CreateProductViewModel : ICreateProductModel
    {
        public string Alias { get; set; }

        public int BrandId { get; set; }

        public int ColorId { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        public string Description { get; set; }

        public int HighlightImageId { get; set; }

        public bool IsHighlighted { get; set; }

        public int MainImageId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<int> SizeIds { get; set; }

        public string SKUCode { get; set; }

        public ProductTargetType TargetType { get; set; }
    }
}
