namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Collections.Generic;

    public interface ICreateProductModel
    {
        string Alias { get; set; }

        int BrandId { get; set; }
        
        int ColorId { get; set; }

        string Description { get; set; }

        int? HighlightImageId { get; set; }
        
        int MainImageId { get; set; }
        
        string Name { get; set; }

        decimal Price { get; set; }

        string SKUCode { get; set; }

        ProductTargetType TargetType { get; set; }

        IEnumerable<int> CategoryIds { get; set; }

        IEnumerable<int> SizeIds { get; set; }

        IEnumerable<int> GalleryImageIds { get; set; }
    }

    public interface IUpdateProductModel : ICreateProductModel
    {
    }

    public interface ISearchProductModel
    {
        string Name { get; set; }

        int? BrandId { get; set; }

        IEnumerable<int> BrandIds { get; set; }

        int? ColorId { get; set; }

        IEnumerable<int> ColorIds { get; set; }

        int? CategoryId { get; set; }

        IEnumerable<int> CategoryIds { get; set; }

        int? SizeId { get; set; }

        IEnumerable<int> SizeIds { get; set; }

        ProductTargetType? TargetType { get; set; }

        ProductStatus? Status { get; set; }

        bool? IsHighlighted { get; set; }

        decimal? PriceFrom { get; set; }

        decimal? PriceTo { get; set; }
    }
}
