namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductSizesManager
    {
        Task<Size> GetByIdAsync(int id);

        IQueryable<Size> GetQuery();

        IQueryable<Size> GetQueryById(int id);

        IQueryable<Size> SearchProductSize(ISearchSizeModel model);

        Task<IManagerActionResultModel<Size>> CreateProductSizeAsync(ICreateSizeModel model);

        Task<IManagerActionResultModel<Size>> UpdateProductSizeAsync(IUpdateSizeModel model);
    }
}
