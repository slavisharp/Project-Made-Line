namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductManager : IProductManager
    {
        private IRepository<Product> repository;
        private IRepository<Brand> brandRepo;
        private IRepository<Image> imageRepo;
        private IRepository<ProductSize> productSizeRepo;
        private IRepository<Size> sizeRepo;
        private IRepository<ProductColor> colorRepo;
        private IRepository<Category> categoryRepo;
        private IRepository<ProductCategory> productCategoryRepo;

        public ProductManager(IRepository<Product> repo, 
                                IRepository<ProductColor> colorRepo,
                                IRepository<Brand> brandRepo,
                                IRepository<Image> imageRepo,
                                IRepository<ProductSize> productSizeRepo,
                                IRepository<Size> sizeRepo,
                                IRepository<Category> categoryRepo,
                                IRepository<ProductCategory> productCategoryRepo)
        {
            this.repository = repo;
            this.brandRepo = brandRepo;
            this.imageRepo = imageRepo;
            this.productSizeRepo = productSizeRepo;
            this.sizeRepo = sizeRepo;
            this.colorRepo = colorRepo;
            this.categoryRepo = categoryRepo;
            this.productCategoryRepo = productCategoryRepo;
        }

        public async Task<IManagerActionResultModel<Product>> CreateProductAsync(ICreateProductModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = new ManagerActionResultModel<Product>();
            try
            {
                var brand = await this.brandRepo.GetByIdAsync(model.BrandId);
                var color = await this.colorRepo.GetByIdAsync(model.ColorId);
                var mainImage = await this.imageRepo.GetByIdAsync(model.MainImageId);
                var hilightImage = model.HighlightImageId != null ? await this.imageRepo.GetByIdAsync(model.HighlightImageId) : mainImage;
                var product = new Product()
                {
                    Alias = model.Alias,
                    Brand = brand,
                    BrandId = model.BrandId,
                    Color = color,
                    ColorId = model.ColorId,
                    Description = model.Description,
                    IsHighlighted = model.IsHighlighted,
                    Price = model.Price,
                    MainImage = mainImage,
                    MainImageId = model.MainImageId,
                    HighlightImage = hilightImage,
                    Name = model.Name,
                    SKUCode = model.SKUCode,
                    TargetType = model.TargetType,
                    Status = ProductStatus.Draft
                };

                if (model.SizeIds != null)
                {
                    var sizes = this.sizeRepo.GetRange(model.SizeIds).ToList();
                    await AddSizesToProduct(product, sizes);
                }

                if (model.CategoryIds != null)
                {
                    var categories = this.categoryRepo.GetRange(model.CategoryIds).ToList();
                    await AddCategoriesToProduct(product, categories);
                }
                
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
        
        private async Task AddSizesToProduct(Product product, List<Size> sizes)
        {
            foreach (var item in sizes)
            {
                await this.productSizeRepo.AddAsync(new ProductSize()
                {
                    Available = true,
                    Product = product,
                    Size = item
                });
            }
        }

        private async Task AddCategoriesToProduct(Product product, List<Category> categories)
        {
            foreach (var item in categories)
            {
                await this.productCategoryRepo.AddAsync(new ProductCategory()
                {
                    Category = item,
                    Product = product
                });
            }
        }
    }
}
