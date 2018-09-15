namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Data.Models;
    using System;
    using System.Linq.Expressions;

    public class SizeDetailsViewModel
    {
        public int Id { get; set; }

        public int ProductsCount { get; set; }

        public string Name { get; set; }
        
        public static Expression<Func<Size, SizeDetailsViewModel>> FromEntity
        {
            get
            {
                return x => new SizeDetailsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductsCount = x.Products.Count
                };
            }
            
        }
    }
}
