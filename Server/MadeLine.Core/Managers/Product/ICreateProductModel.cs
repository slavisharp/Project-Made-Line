namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public interface ICreateProductModel
    {
        string Alias { get; set; }

        int BrandId { get; set; }
        
        int ColorId { get; set; }

        IEnumerable<int> CategoryIds { get; set; }

        string Description { get; set; }

        int? HighlightImageId { get; set; }

        bool IsHighlighted { get; set; }

        int MainImageId { get; set; }
        
        string Name { get; set; }

        decimal Price { get; set; }

        IEnumerable<int> SizeIds { get; set; }

        string SKUCode { get; set; }

        ProductTargetType TargetType { get; set; }
    }
}
