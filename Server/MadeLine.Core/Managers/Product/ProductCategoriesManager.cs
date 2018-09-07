namespace MadeLine.Core.Managers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductCategoriesManager : IProductCategoriesManager
    {
        private readonly IRepository<Category> categoryRepo;
        private readonly AppSettings appSettings;

        public ProductCategoriesManager(IRepository<Category> categoryRepo, IOptions<AppSettings> options)
        {
            this.categoryRepo = categoryRepo;
            this.appSettings = options.Value;
        }

        public async Task<IManagerActionResultModel<Category>> CreateProductCategoryAsync(ICreateCategoryModel model)
        {
            var result = new ManagerActionResultModel<Category>();
            try
            {
                bool isExisting = this.categoryRepo.All().Where(c => c.Name == model.Name).Any();
                if (isExisting)
                {
                    result.Errors.Add(new ErrorResultModel()
                    {
                        Code = nameof(model.Name),
                        Message = $"Category with name {model.Name} already exists!"
                    });
                    return result;
                }

                var entity = new Category()
                {
                    Name = model.Name
                };

                entity.Translations.Add(new CategoryTranslation()
                {
                    Category = entity,
                    Language = this.appSettings.DefaultLanguage,
                    Name = model.Name
                });

                this.categoryRepo.Add(entity);
                await this.categoryRepo.SaveAsync();
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

        public async Task<Category> GetByIdAsync(int id)
        {
            return await this.categoryRepo.GetByIdAsync(id);
        }

        public IQueryable<Category> GetQuery()
        {
            return this.categoryRepo.All();
        }

        public IQueryable<Category> GetQueryById(int id)
        {
            return this.categoryRepo.All().Where(c => c.Id == id);
        }

        public IQueryable<Category> SearchProductCategory(ISearchCategoryModel model)
        {
            var query = this.categoryRepo.All();
            if (model == null)
            {
                return query;
            }
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                if (model.Language != null)
                {
                    query = query.Where(c => c.Translations.Where(t => t.Language == model.Language && t.Name.Contains(model.Name)).Any());
                }
                else
                {
                    query = query.Where(c => c.Name.Contains(model.Name));
                }
            }

            return query;
        }

        public async Task<IManagerActionResultModel<Category>> UpdateProductCategoryAsync(IUpdateCategoryModel model)
        {
            var result = new ManagerActionResultModel<Category>();
            try
            {
                Category entity = await this.GetByIdAsync(model.Id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Errors.Add(new ErrorResultModel(string.Empty, "Category not found!"));
                }

                CategoryTranslation translation = entity.Translations.Where(t => t.Language == this.appSettings.DefaultLanguage).FirstOrDefault();
                entity.Name = model.Name;
                translation.Name = model.Name;
                await this.categoryRepo.SaveAsync();
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

        public async Task<IManagerActionResultModel<Category>> UpdateProductCategoryTranslation(ICategoryTranslationModel model)
        {
            var result = new ManagerActionResultModel<Category>();
            Category entity = await this.GetByIdAsync(model.Id);
            if (entity == null)
            {
                result.Succeeded = false;
                result.Errors.Add(new ErrorResultModel(string.Empty, "Category not found!"));
            }

            if (model.Language == this.appSettings.DefaultLanguage)
            {
                return await UpdateProductCategoryAsync(model);
            }
            else
            {
                CategoryTranslation translation = entity.Translations.Where(t => t.Language == this.appSettings.DefaultLanguage).FirstOrDefault();
                if (translation == null)
                {
                    translation = new CategoryTranslation()
                    {
                        Category = entity,
                        CategoryId = entity.Id,
                        Language = model.Language
                    };
                    entity.Translations.Add(translation);
                }
                
                translation.Name = model.Name;
                await this.categoryRepo.SaveAsync();
            }

            return result;
        }
    }
}
