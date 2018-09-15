namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Data.Models;
    using System;
    using System.Linq.Expressions;

    public class ProductColorDetailsViewModel
    {
        public int Id { get; set; }

        public int ProductsCount { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public static Expression<Func<ProductColor, ProductColorDetailsViewModel>> FromEntity
        {
            get
            {
                return x => new ProductColorDetailsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductsCount = x.Products.Count,
                    Value = x.Value
                };
            }
            
        }
    }
}
