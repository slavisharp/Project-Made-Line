namespace MadeLine.Core.Managers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductColorsManager : IProductColorsManager
    {
        private readonly IRepository<ProductColor> colorRepo;
        private readonly AppSettings appSettings;

        public ProductColorsManager(IRepository<ProductColor> colorRepo, IOptions<AppSettings> options)
        {
            this.colorRepo = colorRepo;
            this.appSettings = options.Value;
        }

        public async Task<IManagerActionResultModel<ProductColor>> CreateProductColorAsync(ICreateColorModel model)
        {
            var result = new ManagerActionResultModel<ProductColor>();
            try
            {
                var entity = new ProductColor()
                {
                    Name = model.Name,
                    Value = model.Value
                };
                
                this.colorRepo.Add(entity);
                await this.colorRepo.SaveAsync();
                result.Succeeded = true;
                result.Model = entity;
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ErrorResultModel(ex));
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ErrorResultModel(ex.InnerException));
                }
            }

            return result;
        }

        public async Task<ProductColor> GetByIdAsync(int id)
        {
            return await this.colorRepo.GetByIdAsync(id);
        }

        public IQueryable<ProductColor> GetQuery()
        {
            return this.colorRepo.All();
        }

        public IQueryable<ProductColor> GetQueryById(int id)
        {
            return this.colorRepo.All().Where(c => c.Id == id);
        }

        public IQueryable<ProductColor> SearchProductColor(ISearchColorModel model)
        {
            var query = this.colorRepo.All();
            if (model == null)
            {
                return query;
            }
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(c => c.Name.Contains(model.Name));
            }

            if (!string.IsNullOrWhiteSpace(model.Value))
            {
                query = query.Where(c => c.Value.Contains(model.Value));
            }

            return query;
        }

        public async Task<IManagerActionResultModel<ProductColor>> UpdateProductColorAsync(IUpdateColorModel model)
        {
            var result = new ManagerActionResultModel<ProductColor>();
            try
            {
                ProductColor entity = await this.GetByIdAsync(model.Id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Errors.Add(new ErrorResultModel(string.Empty, "Product color not found!"));
                }
                
                entity.Name = model.Name;
                entity.Value = model.Value;
                await this.colorRepo.SaveAsync();
                result.Succeeded = true;
                result.Model = entity;
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ErrorResultModel(ex));
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ErrorResultModel(ex.InnerException));
                }
            }

            return result;
        }
    }
}
