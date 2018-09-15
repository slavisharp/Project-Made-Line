namespace MadeLine.Core.Managers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductSizesManager : IProductSizesManager
    {
        private readonly IRepository<Size> sizeRepo;
        private readonly AppSettings appSettings;

        public ProductSizesManager(IRepository<Size> sizeRepo, IOptions<AppSettings> options)
        {
            this.sizeRepo = sizeRepo;
            this.appSettings = options.Value;
        }

        public async Task<IManagerActionResultModel<Size>> CreateProductSizeAsync(ICreateSizeModel model)
        {
            var result = new ManagerActionResultModel<Size>();
            try
            {
                bool isExisting = this.sizeRepo.All().Where(c => c.Name == model.Name).Any();
                if (isExisting)
                {
                    result.Errors.Add(new ErrorResultModel()
                    {
                        Code = nameof(model.Name),
                        Message = $"Size with name {model.Name} already exists!"
                    });
                    return result;
                }

                var entity = new Size()
                {
                    Name = model.Name
                };
                
                this.sizeRepo.Add(entity);
                await this.sizeRepo.SaveAsync();
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

        public async Task<Size> GetByIdAsync(int id)
        {
            return await this.sizeRepo.GetByIdAsync(id);
        }

        public IQueryable<Size> GetQuery()
        {
            return this.sizeRepo.All();
        }

        public IQueryable<Size> GetQueryById(int id)
        {
            return this.sizeRepo.All().Where(c => c.Id == id);
        }

        public IQueryable<Size> SearchProductSize(ISearchSizeModel model)
        {
            var query = this.sizeRepo.All();
            if (model == null)
            {
                return query;
            }
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(c => c.Name.Contains(model.Name));
            }

            return query;
        }

        public async Task<IManagerActionResultModel<Size>> UpdateProductSizeAsync(IUpdateSizeModel model)
        {
            var result = new ManagerActionResultModel<Size>();
            try
            {
                Size entity = await this.GetByIdAsync(model.Id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Errors.Add(new ErrorResultModel(string.Empty, "Size not found!"));
                }
                
                entity.Name = model.Name;
                await this.sizeRepo.SaveAsync();
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
