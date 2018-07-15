namespace MadeLine.Core.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;

    public class ProductManager : IProductManager
    {
        private IRepository<Product> repository;

        public ProductManager(IRepository<Product> repo)
        {
            this.repository = repo;
        }

        public async Task<IManagerActionResultModel<Product>> CreateProductAsync(ICreateProductModel model)
        {
            var result = new ManagerActionResultModel<Product>();
            try
            {

                var product = new Product()
                {
                    Alias = model.Alias,
                    BrandId = model.BrandId,
                    ColorId = model.ColorId,
                    Description = model.Description,
                    HighlightImageId = model.HighlightImageId,
                    IsHighlighted = model.IsHighlighted,
                    Price = model.Price,
                    MainImageId = model.MainImageId,
                    Name = model.Name,
                    SKUCode = model.SKUCode,
                    TargetType = model.TargetType,
                    Status = ProductStatus.Draft
                };
                await this.repository.AddAsync(product);
                await this.repository.SaveAsync();
                result.Succeeded = true;
                result.Model = product;
            }
            catch (Exception ex)
            {
                result.Errors = new List<IErrorResultModel>() { new ErrorResultModel(ex) };
                result.Succeeded = false;
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ErrorResultModel(ex.InnerException));
                }
            }

            return result;
        }
        
        public Task<IManagerActionResultModel<Product>> UpdateProductAsync(IUpdateProductModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await this.repository.GetByIdAsync(id);
        }

        public IQueryable<Product> GetQuery()
        {
            return this.repository.All();
        }

        public IQueryable<Product> GetQueryById(int id)
        {
            return this.repository.All().Where(p => p.Id == id && !p.IsDeleted);
        }

        public IQueryable<Product> SearchProduct(ISearchProductModel model)
        {
            var query = GetQuery();

            if (model.BrandId != null)
            {
                query = query.Where(p => p.BrandId == model.BrandId);
            }
            else if (model.BrandIds != null && model.BrandIds.Any())
            {
                query = query.Where(p => model.BrandIds.Contains(p.BrandId));
            }

            if (model.CategoryId != null)
            {
                //query = query.Where(p => p.Categories.Select(c => c.CategoryId).Contains(model.CategoryId));
            }
            else if (model.CategoryIds != null)
            {

            }

            if (model.ColorId != null)
            {
                query = query.Where(p => p.ColorId == model.ColorId);
            }
            else if (model.ColorIds != null && model.ColorIds.Any())
            {
                query = query.Where(p => model.ColorIds.Contains(p.ColorId));
            }

            if (model.IsHighlighted != null)
            {
                query = query.Where(p => p.IsHighlighted == model.IsHighlighted);
            }

            if (model.Name != null)
            {
                query = query.Where(p => p.Name.Contains(model.Name));
            }

            if (model.PriceFrom != null)
            {
                query = query.Where(p => p.Price >= model.PriceFrom);
            }

            if (model.PriceTo != null)
            {
                query = query.Where(p => p.Price <= model.PriceTo);
            }

            if (model.SizeId != null)
            {

            }
            else if (model.SizeIds != null && model.SizeIds.Any())
            {

            }

            if (model.Status != null)
            {
                query = query.Where(p => p.Status == model.Status);
            }

            if (model.TargetType != null)
            {
                query = query.Where(p => p.TargetType == model.TargetType);
            }
            
            return query;
        }
    }
}
