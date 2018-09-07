namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductCategoriesManager
    {
        Task<Category> GetByIdAsync(int id);

        IQueryable<Category> GetQuery();

        IQueryable<Category> GetQueryById(int id);

        IQueryable<Category> SearchProductCategory(ISearchCategoryModel model);

        Task<IManagerActionResultModel<Category>> CreateProductCategoryAsync(ICreateCategoryModel model);

        Task<IManagerActionResultModel<Category>> UpdateProductCategoryAsync(IUpdateCategoryModel model);

        Task<IManagerActionResultModel<Category>> UpdateProductCategoryTranslation(ICategoryTranslationModel model);
    }
}
