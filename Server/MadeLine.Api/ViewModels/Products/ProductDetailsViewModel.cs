namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Data.Models;
    using System.Collections.Generic;

    public class ProductDetailsViewModel
    {
        public string Url { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<int> SizeIds { get; set; }

        public string SKUCode { get; set; }

        public ProductTargetType TargetType { get; set; }

        // Main image

        // Highlight image

        // Categories 

        // Sizes
    }
}
