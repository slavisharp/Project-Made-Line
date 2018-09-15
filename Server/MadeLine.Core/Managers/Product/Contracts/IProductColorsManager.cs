namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductColorsManager
    {
        Task<ProductColor> GetByIdAsync(int id);

        IQueryable<ProductColor> GetQuery();

        IQueryable<ProductColor> GetQueryById(int id);

        IQueryable<ProductColor> SearchProductColor(ISearchColorModel model);

        Task<IManagerActionResultModel<ProductColor>> CreateProductColorAsync(ICreateColorModel model);

        Task<IManagerActionResultModel<ProductColor>> UpdateProductColorAsync(IUpdateColorModel model);
    }
}
