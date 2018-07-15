namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Collections.Generic;

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
